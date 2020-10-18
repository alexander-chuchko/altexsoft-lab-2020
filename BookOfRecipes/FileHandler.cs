using BookOfRecipes.Interfaces;
using BookOfRecipes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace BookOfRecipes
{
    class FileHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryController categoryController;
        private readonly IReceptController receptController;
        private readonly IIngredientController ingredientController;
        private readonly IObjectDeserializer objectDeserializer;
        private readonly ISubcategoryController subcategoryController;

        public FileHandler(IUnitOfWork unitOfWork, ICategoryController categoryController, IReceptController receptController, IIngredientController ingredientController, IObjectDeserializer objectDeserializer, ISubcategoryController subcategoryController)
        {
            this.unitOfWork = unitOfWork;
            this.categoryController = categoryController;
            this.receptController = receptController;
            this.ingredientController = ingredientController;
            this.objectDeserializer = objectDeserializer;
            this.subcategoryController = subcategoryController;
        }
        //Метод для проверки наличия файлов и в случае их отсутствия создать
        public void UploadingOrCreatingFiles()
        {
            Console.WriteLine("\n\t\t\tВыполняется загрузка книги...\n");
            Thread.Sleep(3000);
            string expansion = ".json";
            string[] allFile = { typeof(Category).Name, typeof(Ingredient).Name, typeof(Subcategory).Name, typeof(Recipe).Name };
            foreach (string nameFile in allFile)
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + nameFile + expansion);
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    //В случае если файлы есть с данным именем, то заносим их в глобальные переменные 
                    switch (nameFile + expansion)
                    {
                        case "Category.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка категорий...\n");
                            Thread.Sleep(1000);
                            unitOfWork.Categories.AddRange(objectDeserializer.DeserializingFile<Category>(path));
                            Console.WriteLine("\n\tСписок категорий загружен.");
                            Console.WriteLine();
                            break;
                        case "Recipe.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка рецептов...\n");
                            Thread.Sleep(1000);
                            unitOfWork.Recipes.AddRange(objectDeserializer.DeserializingFile<Recipe>(path));
                            Console.WriteLine("\n\tСписок рецептов загружен.\n");
                            break;
                        case "Ingredient.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка ингредиентов...\n");
                            Thread.Sleep(1000);
                            unitOfWork.Ingredients.AddRange(objectDeserializer.DeserializingFile<Ingredient>(path));
                            Console.WriteLine("\n\tСписок ингредиентов загружен.\n");
                            break;
                        case "Subcategory.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка подкатегорий...\n");
                            Thread.Sleep(1000);
                            unitOfWork.Subcategories.AddRange(objectDeserializer.DeserializingFile<Subcategory>(path));
                            Console.WriteLine("\n\tСписок подкатегорий загружен.");
                            Console.WriteLine();
                            break;
                    }
                }
                else
                {
                    switch (nameFile + expansion)
                    {
                        case "Category.json":
                            Console.WriteLine("\n\tВ книге нет категорий. Выполняется автоматическое добавление списка категорий...\n");
                            Thread.Sleep(1000);
                            unitOfWork.Categories.AddRange(categoryController.CreateCategories());
                            Console.WriteLine("\n\tСписок категорий добавлен и готов к использованию.\n");
                            break;
                        case "Recipe.json":
                            Console.WriteLine("\n\tВ книге нет рецептов. Приступаем к созданию рецептов!\n");
                            unitOfWork.Recipes.AddRange(receptController.CreateRecipe());
                            break;
                        case "Ingredient.json":
                            Console.WriteLine("\n\tВ книге нет ингредиентов. Выполняется автоматическое добавление списка ингредиентов...\n");
                            Thread.Sleep(1000);
                            unitOfWork.Ingredients.AddRange(ingredientController.CreateIngredients());
                            Console.WriteLine("\n\tСписок ингредиентов добавлен и готов к использованию.\n");
                            break;
                        case "Subcategory.json":
                            Console.WriteLine("\n\tВ книге нет подкатегорий. Выполняется автоматическое добавление списка подкатегорий...\n");
                            Thread.Sleep(1000);
                            unitOfWork.Subcategories.AddRange(subcategoryController.CreateSubcategories());
                            Console.WriteLine("\n\tСписок категорий добавлен и готов к использованию.\n");
                            break;
                    }
                }
            }
        }
    }
}
