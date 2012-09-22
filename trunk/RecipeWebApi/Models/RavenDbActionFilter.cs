using System.Web.Http;
using System.Web.Http.Filters;
using Raven.Client;

namespace RecipeWebApi.Models
{
    public class RavenDbActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as ExtendedApiController;
            if (controller == null)
                return;

            using (controller.Session)
            {
                if (controller.Session != null)
                    controller.Session.SaveChanges();
            }
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as ExtendedApiController;
            if (controller == null)
                return;

            controller.Session = WebApiApplication.Store.OpenSession();
        }
        
    }

    public class ExtendedApiController : ApiController
    {
        public IDocumentSession Session { get; set; }
    }
}