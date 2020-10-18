using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface IIngredientController
    {
        List<Ingredient> CreateIngredients();
        Ingredient CreateIngredient(List<Ingredient> modelIngredients);
        List<int> FormationListIndices(List<Ingredient> modelIngredients);
    }

}
