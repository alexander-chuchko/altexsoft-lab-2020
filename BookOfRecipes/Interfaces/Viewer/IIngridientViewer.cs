using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface IIngridientViewer
    {
        void PrintIngridient(List<Ingredient> modelIngredients);
    }
}
