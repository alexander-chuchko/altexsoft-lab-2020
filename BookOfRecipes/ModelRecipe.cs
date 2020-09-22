using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    [Serializable]
    class ModelRecipe: EntityBase
    {
        public string NameRecept { get; set; }
        public string RecipeDescription { get; set; }
        public List<string> RecipeSteps { get; set; }
        public List<int> IdIngredient { get; set; }
        public int IdСategory { get; set; }
    }    
}
