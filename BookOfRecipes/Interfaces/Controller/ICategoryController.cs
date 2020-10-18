using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface ICategoryController
    {
        List<Category> CreateCategories();
        Category CreateCategory(List<Category> modelCategories);
        int CheckingCategoryIndex();
    }
}
