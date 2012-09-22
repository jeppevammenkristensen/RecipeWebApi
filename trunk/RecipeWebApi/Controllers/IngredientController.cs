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
        public IEnumerable<Ingredient> Get()
        {
            return Session.Query<Ingredient>().Take(50);
        }

        // GET api/ingredient/5
        public Ingredient Get(string id)
        {
            return Session.Load<Ingredient>(id);
        }

        // POST api/ingredient
        public void Post([FromBody]Ingredient value)
        {
            Session.Store(value);
        }

        // PUT api/ingredient/5
        public void Put(string id, [FromBody]Ingredient value)
        {
            var result = Session.Load<Ingredient>("ingredients/" + id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Session.Store(value);
        }

        // DELETE api/ingredient/5
        public void Delete(string id)
        {
            var ingredient = Session.Query<Ingredient>(id);
            if (ingredient == null)
                return;

            Session.Delete(ingredient);
        }
    }
}
