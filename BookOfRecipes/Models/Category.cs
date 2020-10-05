using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    [Serializable]
    class Category : EntityBase
    {
        public string NameCategory { get; set; }
    }
}
