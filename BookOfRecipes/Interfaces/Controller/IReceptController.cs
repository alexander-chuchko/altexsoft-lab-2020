using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface IReceptController
    {
        List<int> GetIndicesByCategory(int categoryIndex);
        List<int> GetIndicesBySubcategory(int subcategoryIndex);
        List<string> AddRecipeSteps(List<string> recipeSteps);
        List<Recipe> CreateRecipe();
    }
}
