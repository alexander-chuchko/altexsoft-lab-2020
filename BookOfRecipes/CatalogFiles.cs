﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace BookOfRecipes
{
    class CatalogFiles
    {
        //Метод для проверки наличия файлов и в случае их отсутствия создать
        public void HandlingFile(UnitOfWork unitOfWork)
        {
            Console.WriteLine("\n\t\t\tВыполняется загрузка книги...\n");
            Thread.Sleep(3000);
            string[] allFile = { "category.json", "ingredient.json", "recipe.json" };
            foreach (string nameFile in allFile)
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + nameFile;
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    //В случае если файлы есть с данным именем, то заносим их в глобальные переменные 
                    switch (nameFile)
                    {
                        case "category.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка категорий...\n");
                            Thread.Sleep(1000);
                            ObjectDeserializer<ModelCategory> categoryDetails = new ObjectDeserializer<ModelCategory>();
                            unitOfWork.contextEntity.CategorySheet = categoryDetails.DeserializingFile(path);
                            Console.WriteLine("\n\tСписок категорий загружен.");
                            Console.WriteLine();
                            break;
                        case "recipe.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка рецептов...\n");
                            Thread.Sleep(1000);
                            ObjectDeserializer<ModelRecipe> recipeDetails = new ObjectDeserializer<ModelRecipe>();
                            unitOfWork.contextEntity.RecipeSheet = recipeDetails.DeserializingFile(path);
                            Console.WriteLine("\n\tСписок рецептов загружен.\n");
                            break;
                        case "ingredient.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка ингредиентов...\n");
                            Thread.Sleep(1000);
                            ObjectDeserializer<ModelIngredient> ingredientDetails = new ObjectDeserializer<ModelIngredient>();
                            unitOfWork.contextEntity.IngredientSheet = ingredientDetails.DeserializingFile(path);
                            Console.WriteLine("\n\tСписок ингредиентов загружен.\n");
                            break;
                    }
                }
                else
                {
                    switch (nameFile)
                    {
                        case "category.json":
                            Console.WriteLine("\n\tВ книге нет категорий. Выполняется автоматическое добавление списка категорий...\n");
                            Thread.Sleep(1000);
                            CategoryController categoryController = new CategoryController();
                            unitOfWork.repositoryCategory.AddRange(categoryController.CreateCategories());
                            Console.WriteLine("\n\tСписок категорий добавлен и готов к использованию.\n");
                            break;
                        case "recipe.json":
                            Console.WriteLine("\n\tВ книге нет рецептов. Приступаем к созданию рецептов!\n");
                            ViewIngridient viewIngridient = new ViewIngridient();
                            ViewCategory viewCategory = new ViewCategory();
                            ReceptController receptController = new ReceptController(viewIngridient,viewCategory);
                            unitOfWork.repositoryRecipe.AddRange(receptController.CreateRecipe(unitOfWork));
                            //unitOfWork.repositoryRecipe.AddRange(ReceptController.CreateRecipe(unitOfWork));
                            break;
                        case "ingredient.json":
                            Console.WriteLine("\n\tВ книге нет ингредиентов. Выполняется автоматическое добавление списка ингредиентов...\n");
                            Thread.Sleep(1000);
                            IngredientController ingredientController = new IngredientController();
                            unitOfWork.repositoryIngredient.AddRange(ingredientController.CreateIngredients());
                            Console.WriteLine("\n\tСписок ингредиентов добавлен и готов к использованию.\n");
                            break;
                    }
                }
            }
        }
    }
}
