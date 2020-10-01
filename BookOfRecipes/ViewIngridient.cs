using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BookOfRecipes
{
    class ViewIngridient
    {
        //Метод выводит список ингредиентов на консоль
        public void PrintIngridient(List<ModelIngredient> modelIngredients)
        {
            if (modelIngredients.Count > 0)
            {
                for (int i = 0; i < modelIngredients.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", modelIngredients[i].Id, modelIngredients[i].NameIngredient);
                }
            }
            else
            {
                Console.WriteLine("В данном списке нет ингредиентов.");
            }
        }
    }

}