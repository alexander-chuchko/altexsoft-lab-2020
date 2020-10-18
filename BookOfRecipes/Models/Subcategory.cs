using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Models
{
    class Subcategory:EntityBase, ISaveble
    {
        public int IdCategory { get; set; }
    }
}
