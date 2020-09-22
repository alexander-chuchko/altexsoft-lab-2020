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
        public static void HandlingFile(UnitOfWork unitOfWork)
        {
            Console.WriteLine("\n\t\t\tВыполняется загрузка книги...\n");
            Thread.Sleep(3000);
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
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка категорий...\n");
                            Thread.Sleep(1000);
                            ObjectDeserializer<ModelCategory> categoryDetails = new ObjectDeserializer<ModelCategory>();
                            unitOfWork.contextEntity.CategorySheet = categoryDetails.DeserializingFile(path);
                            Console.WriteLine("\n\tСписок категорий загружен.");
                            Console.WriteLine();
                            break;
                        case "recept.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка рецептов...\n");
                            Thread.Sleep(1000);
                            ObjectDeserializer<ModelRecipe> recipeDetails = new ObjectDeserializer<ModelRecipe>();
                            unitOfWork.contextEntity.RecipeSheet= recipeDetails.DeserializingFile(path);
                            Console.WriteLine("\n\tСписок рецептов загружен.\n");
                            break;
                        case "ingridient.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка ингредиентов...\n");
                            Thread.Sleep(1000);
                            ObjectDeserializer<ModelIngredient> ingredientDetails = new ObjectDeserializer<ModelIngredient>();
                            unitOfWork.contextEntity.IngredientSheet= ingredientDetails.DeserializingFile(path);
                            Console.WriteLine("\n\tСписок ингредиентов загружен.\n");
                            break;
                    }
                }
                else
                {
                    switch (nameFile)
                    {
                        case "categoty.json":
                            Console.WriteLine("\n\tВ книге нет категорий. Выполняется автоматическое добавление списка категорий...\n");
                            Thread.Sleep(1000);
                            unitOfWork.repositoryCategory.AddRange(CategoryController.CreateCategories());
                            Console.WriteLine("\n\tСписок категорий добавлен и готов к использованию.\n");
                            break;
                        case "recept.json":
                            Console.WriteLine("\n\tВ книге нет рецептов. Приступаем к созданию рецептов!\n");
                            unitOfWork.repositoryReciipe.AddRange(ReceptController.CreateRecipe(unitOfWork));
                            break;
                        case "ingridient.json":
                            Console.WriteLine("\n\tВ книге нет ингредиентов. Выполняется автоматическое добавление списка ингредиентов...\n");
                            Thread.Sleep(1000);
                            unitOfWork.repositoryIngredient.AddRange(IngredientController.CreateIngredients());
                            Console.WriteLine("\n\tСписок ингредиентов добавлен и готов к использованию.\n");
                            break;
                    }
                }
            }
        }
    }
}