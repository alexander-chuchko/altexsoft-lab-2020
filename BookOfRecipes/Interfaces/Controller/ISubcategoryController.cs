using BookOfRecipes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface ISubcategoryController
    {
        List<Subcategory> CreateSubcategories();
        Subcategory CreateSubcategory(List<Subcategory> modelSubcategories);
        int CheckingSubcategoryIndex();
    }
}
