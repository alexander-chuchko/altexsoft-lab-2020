using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using BookOfRecipes;

static class ReceptController
    {
        public static string AddName()
        {
            Console.WriteLine("\n\tВведите имя рецепта: \n");
            string userMessage = "Введите имя рецепта";
            for (; ; )
            {
                string inputName = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputName))
                {
                    return inputName;
                }
                else
                {
                    Console.WriteLine("\n\tВведите имя рецепта: \n");
                    return userMessage;
                }
            }
        }
        //Метод для формирования шагов приготовления рецепта 
        public static List<string> AddRecipeSteps(List<string> recipeSteps)
        {
            Console.WriteLine("\n\tНеобходимо ввести шаги приготовления рецепта. По окончанию формирования списка шагов введите - 'e'" +
                    "\n\tВведите шаги приготовления рецепта:\n");
            for (int i = 1; ; i++)
            {
                Console.Write("\t" + i + ". ");
                string inputStep = Console.ReadLine();
                if (inputStep == "e")
                {
                    return recipeSteps;
                }
                else if (!string.IsNullOrEmpty(inputStep))
                {
                    recipeSteps.Add(inputStep);
                }
                else
                {
                    Console.WriteLine("\n\tВведите шаг приготовления рецепта: \n");
                    continue;
                }
            }
        }
        //Метод для добавления формирования описания рецепта
        public static string AddDescription()
        {
            string userMessage = "Введите описание рецепта";
            Console.WriteLine("\n\t{0}:", userMessage);
            string newScript = Console.ReadLine();
            if (!string.IsNullOrEmpty(newScript))
            {
                return newScript;
            }
            else
            {
                Console.WriteLine("\n\tОписание рецепта не введено");
                return userMessage;
            }
        }

        //Метод для добавления рецептов. Рецепты добавляет пользователь на основании созданных категорий и ингридиентов
        public static List<ModelRecipe> CreateRecipe()
        {
            List<ModelRecipe> listModelRecipes = new List<ModelRecipe>();
            ConsoleKeyInfo keypress;
            do
            {
                //Environment.Exit(0);

                //Создаем экземпляр класса ModelRecipe
                ModelRecipe modelRecipe = new ModelRecipe();
                //Выводим на консоль название категорий
                ViewCategory.PrintingСategories();
                //Добавляем индекс категории
                modelRecipe.idCategoty = CategoryController.CheckingCategoryIndex();
                //Добавляем имя рецепту
                modelRecipe.nameRecept = ReceptController.AddName();
                Console.WriteLine("\n\tВыбирте номер ингридиентa из списка:\n");
                //Механизм вывода списка ингридиентов на консоль
                ViewIngridient.PrintIngridient();
                //Выделяем динамическую память под объект
                modelRecipe.idIngredient = new List<int>();
                //Добавляем в лист с индексами указанных ингредиентов. Дубликаты не допускаются
                modelRecipe.idIngredient.AddRange(IngredientController.FormationListIndices().Distinct().ToArray());
                //Добавляем описание рецепта
                modelRecipe.recipeDescription = ReceptController.AddDescription();
                //Выделяем динамическую память
                modelRecipe.recipeSteps = new List<string>();
                //Добавляем в рецепт шаги приготовления рецепта. Повторение шагов не допускается
                modelRecipe.recipeSteps.AddRange(ReceptController.AddRecipeSteps(modelRecipe.recipeSteps).Distinct().ToArray());
                //Добавляем новый рецепт в переменную
                listModelRecipes.Add(modelRecipe);
                Console.WriteLine("\n\tДля введения следующего рецепта нажмите - 'Enter'" +
                                    "\n\tДля выхода в главное меню 'e'\n");
                keypress = Console.ReadKey();
            } while (keypress.KeyChar != 'e');
            return listModelRecipes;
        }
    }

