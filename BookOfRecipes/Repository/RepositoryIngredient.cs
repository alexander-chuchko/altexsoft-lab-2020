using BookOfRecipes.Interfaces;
using BookOfRecipes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes
{
    class RepositoryIngredient : RepositoryBase
    {
        public RepositoryIngredient(ContextEntity contextEntity) : base(contextEntity)
        {

        }
    }
}