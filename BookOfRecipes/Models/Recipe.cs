using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    [Serializable]
    class Recipe : EntityBase, ISaveble
    {
        public string Description { get; set; }
        public List<string> Steps { get; set; }
        public List<int> IdIngredient { get; set; }
        public int IdСategory { get; set; }
        public int? IdSubcategory { get; set; }
    }
}
