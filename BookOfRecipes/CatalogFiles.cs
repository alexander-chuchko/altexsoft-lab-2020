using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using BookOfRecipes;

static class CatalogFiles
    {
        //Метод для проверки наличия файлов и в случае их отсутствия создать
        public static void HandlingFile()
        {
            string[] allFile = { "categoty.json", "ingridient.json", "recept.json" };

            foreach (string nameFile in allFile)
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + nameFile;
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    //В случае если файлы есть с данным именем, то заносим их в глобальные переменные 
                    switch (nameFile)
                    {
                        case "categoty.json":
                            ObjectDeserializer<ModelCategory> categoryDetails = new ObjectDeserializer<ModelCategory>();
                            SaveList.categorySheet = categoryDetails.DeserializingFile(path);
                            break;
                        case "recept.json":
                            ObjectDeserializer<ModelRecipe> recipeDetails = new ObjectDeserializer<ModelRecipe>();
                            SaveList.recipeSheet = recipeDetails.DeserializingFile(path);
                            SaveList.pathRecipe = path;
                            break;
                        case "ingridient.json":
                            ObjectDeserializer<ModelIngredient> ingredientDetails = new ObjectDeserializer<ModelIngredient>();
                            SaveList.ingredientSheet = ingredientDetails.DeserializingFile(path);
                            break;
                    }
                }
                else
                {
                    switch (nameFile)
                    {
                        case "categoty.json":
                            SaveList.categorySheet = CategoryController.CreateCategories();
                            ObjectSerializer<ModelCategory> categoryDetails = new ObjectSerializer<ModelCategory>();
                            categoryDetails.SerializingFile(SaveList.categorySheet, path);
                            break;
                        case "recept.json":
                            Console.WriteLine("\n\tВ книге нет рецептов. Приступаем к созданию рецептов!\n");
                            SaveList.recipeSheet = ReceptController.CreateRecipe();
                            ObjectSerializer<ModelRecipe> receptDetails = new ObjectSerializer<ModelRecipe>();
                            receptDetails.SerializingFile(SaveList.recipeSheet, path);
                            SaveList.pathRecipe = path;
                            break;
                        case "ingridient.json":
                            SaveList.ingredientSheet = IngredientController.CreateIngredient();
                            ObjectSerializer<ModelIngredient> ingredientDetails = new ObjectSerializer<ModelIngredient>();
                            ingredientDetails.SerializingFile(SaveList.ingredientSheet, path);
                            break;
                    }
                }
            }
        }
    }

