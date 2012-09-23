using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web.Http.Filters;

namespace RecipeWebApi.Models
{
    public static class QueryableAttributeHelper
    {
        public static void EffectQueryable<TObject>(this HttpActionExecutedContext actionExecutedContext,params OrderElement<TObject>[] sortResolvers)
        {
            IQueryable<TObject> source;
            if (actionExecutedContext.Response.TryGetContentValue(out source))
            {
                var queryStrings = actionExecutedContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key.ToUpper(),
                                                                                                       x => x.Value);

                int page = 1;
                int pageSize = 100;
                string orderby = string.Empty;
                string orderbyDesc = string.Empty;

                if (queryStrings.ContainsKey("PAGE"))
                    int.TryParse(queryStrings["PAGE"], out page);

                if (queryStrings.ContainsKey("PAGESIZE"))
                    int.TryParse(queryStrings["PAGESIZE"], out pageSize);

                if (queryStrings.ContainsKey("ORDERBY"))
                    orderby = queryStrings["ORDERBY"];

                if (queryStrings.ContainsKey("ORDERBYDESC"))
                    orderbyDesc = queryStrings["ORDERBYDESC"];

                var result = source.
                    Skip((page - 1)*pageSize).Take(pageSize);

                if (!string.IsNullOrEmpty(orderby))
                {
                    var exp = sortResolvers.First(x => x.Key == orderby.ToUpper());
                    if (exp != null)
                        result = result.OrderBy(exp.KeySelector);
                }
                else if (!string.IsNullOrEmpty(orderbyDesc))
                {
                    var exp = sortResolvers.FirstOrDefault(x => x.Key == orderbyDesc.ToUpper());
                    if (exp != null)
                        result = result.OrderByDescending(exp.KeySelector);
                }

                ((ObjectContent) actionExecutedContext.Response.Content).Value = result;
            }
        }
    }

    public class OrderElement<TObject>
    {
        private readonly Lazy<string> _key; 
        
        public OrderElement(Expression<Func<TObject, object>> keySelector)
        {
            KeySelector = keySelector;
            _key = new Lazy<string>(() => GetKey().ToUpper());
        }

        public Expression<Func<TObject, object>> KeySelector { get; set; }

        public string Key
        {
            get { return _key.Value; }
        }

        private string GetKey()
        {
            MemberExpression memberExpression;

            if (KeySelector.Body is UnaryExpression)
                memberExpression = (MemberExpression)((UnaryExpression)KeySelector.Body).Operand;
            else if (KeySelector.Body is MemberExpression)
                memberExpression = (MemberExpression)KeySelector.Body;
            else
                throw new InvalidOperationException("Property name can only be parsed on MemberExpression.");

            return memberExpression.Member.Name;
        }
    }

    public class IngredientFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.EffectQueryable(
                new OrderElement<Ingredient>(x => x.Id),
                new OrderElement<Ingredient>(x => x.DisplayName), 
                new OrderElement<Ingredient>(x => x.Description));
        }   
    }
}