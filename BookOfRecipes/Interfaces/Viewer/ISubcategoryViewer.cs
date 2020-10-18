using BookOfRecipes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface ISubcategoryViewer
    {
        void PrintingSubcategories(List<Subcategory> modelSubcategories, int indexCategory);
        void PrintingAllSubcategories(List<Subcategory> subcategories, List<Category> categories);
    }
}
