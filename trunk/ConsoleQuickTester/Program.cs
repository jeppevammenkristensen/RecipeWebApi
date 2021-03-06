﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary;

namespace ConsoleQuickTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
             DoAsyncStuff();

            while (true)
            {
                
            }
        }

        private static async void DoAsyncStuff()
        {
            var client = new HttpClient();

            var ingredient = new Ingredient() {Description = "Direkte fra koen", DisplayName = "Mælk"};
            //ingredient.StorageOptions.Add(new StorageOption{Comment = "bla bla",Location = "Køleskab", TimeSpan = TimeSpan.FromDays(6)});

            var content = new ObjectContent<Ingredient>(ingredient, new JsonMediaTypeFormatter());
            var result = await client.PutAsync("http://localhost:54295/api/ingredient/98",content);
            //var list = await result.Content.ReadAsAsync<Ingredient[]>(new List<JsonMediaTypeFormatter> {new JsonMediaTypeFormatter()});

            //var result = await client.PostAsync(@"http://localhost:54295/api/ingredient", content);

            int i = 0;

        }
    }
}
