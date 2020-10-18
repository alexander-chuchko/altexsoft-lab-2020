using BookOfRecipes.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes
{
    interface IUnitOfWork
    {
        void Commit();
        RepositoryCategory Categories
        {
            get;
        }
        RepositoryIngredient Ingredients
        {
            get;
        }
        RepositoryRecipe Recipes
        {
            get;
        }
        RepositorySubcategory Subcategories
        {
            get;
        }
    }
}
