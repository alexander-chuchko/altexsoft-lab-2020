using BookOfRecipes.Interfaces;
using Newtonsoft.Json;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryViewer categoryViewer;
        private readonly IIngridientViewer ingridientViewer;
        public CatalogFiles(IUnitOfWork unitOfWork, ICategoryViewer categoryViewer, IIngridientViewer ingridientViewer)
        {
            this.unitOfWork = unitOfWork;
            this.categoryViewer = categoryViewer;
            this.ingridientViewer = ingridientViewer;
        }
        //Метод для проверки наличия файлов и в случае их отсутствия создать
        public void HandlingFile()
        {
            Console.WriteLine("\n\t\t\tВыполняется загрузка книги...\n");
            Thread.Sleep(3000);
            string[] allFile = { "category.json", "ingredient.json", "recipe.json" };
            foreach (string nameFile in allFile)
            {
                string path =Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\"+nameFile);
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    //В случае если файлы есть с данным именем, то заносим их в глобальные переменные 
                    switch (nameFile)
                    {
                        case "category.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка категорий...\n");
                            Thread.Sleep(1000);
                            ObjectDeserializer<Category> categoryDetails = new ObjectDeserializer<Category>();
                            unitOfWork.GetLink().GetCategory.AddRange(categoryDetails.DeserializingFile(path));
                            Console.WriteLine("\n\tСписок категорий загружен.");
                            Console.WriteLine();
                            break;
                        case "recipe.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка рецептов...\n");
                            Thread.Sleep(1000);
                            ObjectDeserializer<Recipe> recipeDetails = new ObjectDeserializer<Recipe>();
                            unitOfWork.GetLink().GetRecipe.AddRange(recipeDetails.DeserializingFile(path));
                            Console.WriteLine("\n\tСписок рецептов загружен.\n");
                            break;
                        case "ingredient.json":
                            Console.WriteLine("\n\tВыполняется загрузка существующего списка ингредиентов...\n");
                            Thread.Sleep(1000);
                            ObjectDeserializer<Ingredient> ingredientDetails = new ObjectDeserializer<Ingredient>();
                            unitOfWork.GetLink().GetIngredient.AddRange(ingredientDetails.DeserializingFile(path));
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
                            CategoryController categoryController = new CategoryController(unitOfWork);
                            unitOfWork.GetLink().GetCategory.AddRange(categoryController.CreateCategories());
                            Console.WriteLine("\n\tСписок категорий добавлен и готов к использованию.\n");
                            break;
                        case "recipe.json":
                            Console.WriteLine("\n\tВ книге нет рецептов. Приступаем к созданию рецептов!\n");
                            ReceptController receptController = new ReceptController(ingridientViewer, categoryViewer, unitOfWork);
                            unitOfWork.GetLink().GetRecipe.AddRange(receptController.CreateRecipe());
                            break;
                        case "ingredient.json":
                            Console.WriteLine("\n\tВ книге нет ингредиентов. Выполняется автоматическое добавление списка ингредиентов...\n");
                            Thread.Sleep(1000);
                            IngredientController ingredientController = new IngredientController();
                            unitOfWork.GetLink().GetIngredient.AddRange(ingredientController.CreateIngredients());

                            Console.WriteLine("\n\tСписок ингредиентов добавлен и готов к использованию.\n");
                            break;
                    }
                }
            }
        }
    }
}
