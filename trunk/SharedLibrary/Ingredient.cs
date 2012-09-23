using System.Collections.Generic;

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
}