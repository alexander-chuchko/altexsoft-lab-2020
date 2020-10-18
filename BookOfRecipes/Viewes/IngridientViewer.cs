using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BookOfRecipes
{
    class IngridientViewer : IIngridientViewer
    {
        //Метод выводит список ингредиентов на консоль
        public void PrintIngridient(List<Ingredient> modelIngredients)
        {
            if (modelIngredients.Count > 0)
            {
                for (int i = 0; i < modelIngredients.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", modelIngredients[i].Id, modelIngredients[i].Name);
                }
            }
            else
            {
                Console.WriteLine("В данном списке нет ингредиентов.");
            }
        }
    }
}