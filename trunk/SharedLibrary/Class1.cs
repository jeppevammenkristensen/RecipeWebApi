using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedLibrary
{
    public class Ingredient
    {
        public Ingredient()
        {
            StorageOptions = new List<StorageOption>();
        }

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public List<StorageOption> StorageOptions { get; set; }
    }

    public class StorageOption
    {
        public string Location { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string Comment { get; set; }
    }
}
