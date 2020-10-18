using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface IRecipeViewer
    {
        int PrintRecipesBySubсategory(int subcategoryNumber, List<Recipe> modelRecipes);
        int PrintRecipesByСategory(int categoryNumber, List<Recipe> modelRecipes);
        void PrintRecipeDetails(List<int> idRecipes, List<Recipe> modelRecipes, List<Ingredient> modelIngredients);
    }
}
