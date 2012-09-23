using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeWebApi.Models;

namespace RecipeWebApi.Controllers
{
    
    public class IngredientController : ExtendedApiController
    {
        // GET api/ingredient

        [IngredientFilterAttribute]
        public IQueryable<Ingredient> Get()
        {
            return Session.Query<Ingredient>().Take(50);
        }

        [IngredientFilterAttribute]
        // GET api/ingredient/5
        public Ingredient Get(string id)
        {
            return Session.Load<Ingredient>(EnsureId(id));
        }

        // POST api/ingredient
        public void Post([FromBody]Ingredient value)
        {
            Session.Store(value);
        }

        // PUT api/ingredient/5
        public void Put(string id, [FromBody]Ingredient value)
        {
            var result = Session.Load<Ingredient>(EnsureId(id));
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            result.Map(value);

            Session.Store(value);
        }

        // DELETE api/ingredient/5

        public void Delete(string id)
        {
            var ingredient = Session.Load<Ingredient>(EnsureId(id));
            if (ingredient == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Session.Delete(ingredient);
        }

        private static string EnsureId(string id)
        {
            if (!id.StartsWith("ingredients/"))
                return "ingredients/" + id;
            return id;
        }
    }
}
