using BookOfRecipes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes
{
    class RepositoryRecipe : RepositoryBase
    {
        public RepositoryRecipe(ContextEntity contextEntity) : base(contextEntity)
        {

        }
    }
}