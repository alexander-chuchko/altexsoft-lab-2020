using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;


    class ViewRecipe
    {
        public static int PrintRecipesByСategory(int categoryNumber)
        {
            int counter = 0;
            Console.WriteLine("\n\tВыводим имена рецептов согласно указанной категории:\n");
            for (int i = 0; i < SaveList.recipeSheet.Count; i++)
            {
                if (SaveList.recipeSheet[i].idCategoty == categoryNumber)
                {
                    //Выводим имена рецептов
                    Console.WriteLine(string.Format("\n\t\t{0} - {1}\n\n", i + 1, SaveList.recipeSheet[i].nameRecept));
                    counter++;
                }
            }
            Console.WriteLine("\n\tВ данном списке {0} рецептов.\n", counter);
            return counter;
        }
        public static void PrintRecipeDetails(List<int> idRecipes)
        {
            if (SaveList.recipeSheet.Count > 0)
            {
                Console.WriteLine(string.Format("\n\tЧтобы перейти на детали рецепта укажите номер:\n\n"));
                string numberRecept = Console.ReadLine();

                if (int.TryParse(numberRecept, out int result) && SaveList.recipeSheet.Count >= result && result > 0 && idRecipes.Contains(result))
                {
                    Console.WriteLine(string.Format("\n\t\tНазвание: {0}\n\n\t\tОписание: {1}\n\n\t\tШаги:{2}\n", SaveList.recipeSheet[result - 1].nameRecept, SaveList.recipeSheet[result - 1].recipeDescription, string.Join(" ", SaveList.recipeSheet[result - 1].recipeSteps)));
                    Console.WriteLine("\n\t\tИнгридиенты: ");
                    //Выводим список ингридиентов выбранного пользователем рецепта
                    foreach (int i in SaveList.recipeSheet[result - 1].idIngredient)
                    {
                        Console.WriteLine("\t\t" + string.Join(" ", SaveList.ingredientSheet[i].nameIngredient));
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

