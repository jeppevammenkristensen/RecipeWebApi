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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/ingredient/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/ingredient
        public void Post([FromBody]string value)
        {
        }

        // PUT api/ingredient/5
        public void Put(int id, [FromBody]Ingredient value)
        {
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
