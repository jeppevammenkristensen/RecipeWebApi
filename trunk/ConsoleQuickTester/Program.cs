using System;
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

            var ingredient = new Ingredient() {Description = "En alsidig løg", DisplayName = "løg"};
            ingredient.StorageOptions.Add(new StorageOption{Comment = "bla bla",Location = "Stuetemperatur", TimeSpan = TimeSpan.FromDays(8)});
            var content = new ObjectContent<Ingredient>(ingredient, new JsonMediaTypeFormatter());
            var result = await client.PutAsync(@"http://localhost:54295/api/ingredient/1", content);

            var str = await result.Content.ReadAsAsync<string>();

            int i = 0;

        }
    }
}
