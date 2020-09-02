using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using BookOfRecipes;

namespace BookOfRecipes
{
   
        [Serializable]
        class ModelRecipe
        {
            public string nameRecept { get; set; }
            public string recipeDescription { get; set; }
            public List<string> recipeSteps { get; set; }
            public List<int> idIngredient { get; set; }
            public int idCategoty { get; set; }
        }
    
}
