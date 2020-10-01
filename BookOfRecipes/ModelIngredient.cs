using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    [Serializable]
    class ModelIngredient : EntityBase
    {
        public string NameIngredient { get; set; }
    }
}
