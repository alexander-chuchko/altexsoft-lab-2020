using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes
{
    [Serializable]
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}