using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    class RecipeViewer: IRecipeViewer
    {
        RecipeViewer IRecipeViewer.GetLink()
        {
            RecipeViewer recipeViewer = new RecipeViewer();
            return recipeViewer;
        }
        public int PrintRecipesByСategory(int categoryNumber, List<Recipe> modelRecipes)
        {
            int counter = 0;
            Console.WriteLine("\n\tВыводим имена рецептов согласно указанной категории:\n");
            for (int i = 0; i < modelRecipes.Count; i++)
            {
                if (modelRecipes[i].IdСategory == categoryNumber)
                {
                    //Выводим имена рецептов
                    Console.WriteLine(string.Format("\n\t\t{0} - {1}\n\n", i + 1, modelRecipes[i].NameRecept));
                    counter++;
                }
            }
            Console.WriteLine("\n\tВ данном списке {0} рецептов.\n", counter);
            return counter;
        }
        public void PrintRecipeDetails(List<int> idRecipes, List<Recipe> modelRecipes, List<Ingredient> modelIngredients)
        {
            if (modelRecipes.Count > 0)
            {
                Console.WriteLine(string.Format("\n\tЧтобы перейти на детали рецепта укажите номер:\n\n"));
                string numberRecept = Console.ReadLine();

                if (int.TryParse(numberRecept, out int result) && modelRecipes.Count >= result && result > 0 && idRecipes.Contains(result))
                {
                    Console.WriteLine(string.Format("\n\t\tНазвание: {0}\n\n\t\tОписание: {1}\n\n\t\tШаги:{2}\n", modelRecipes[result - 1].NameRecept, modelRecipes[result - 1].RecipeDescription, string.Join(" ", modelRecipes[result - 1].RecipeSteps)));
                    Console.WriteLine("\n\t\tИнгридиенты: ");
                    //Выводим список ингридиентов выбранного пользователем рецепта
                    foreach (int i in modelRecipes[result - 1].IdIngredient)
                    {
                        Console.WriteLine("\t\t" + string.Join(" ", modelIngredients[i - 1].NameIngredient));
                    }
                }
                else
                {
                    Console.WriteLine("Введен некорректный номер рецепта!");
                    return;
                }
            }
        }
    }
}