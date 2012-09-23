using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeWebApi.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            StorageOptions = new List<StorageOption>();
        }

        public string Id { get; set; }
        
        [Required]
        public string DisplayName { get; set;  }
        public string Description { get; set; }

        public List<StorageOption> StorageOptions { get; set; }

        public void Map(Ingredient value)
        {
            Description = value.Description;
            DisplayName = value.DisplayName;
            StorageOptions = value.StorageOptions;
        }
    }

    public class StorageOption
    {
        public string Location { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string Comment { get; set; }
    }
}