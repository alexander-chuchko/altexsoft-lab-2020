using BookOfRecipes.Interfaces;
using BookOfRecipes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    class ReceptController : IReceptController
    {
        private readonly ICategoryViewer categoryViewer;
        private readonly IIngridientViewer ingridientViewer;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryController categoryController;
        private readonly ISubcategoryViewer subcategoryViewer;
        private readonly ISubcategoryController subcategoryController;
        public ReceptController(IIngridientViewer ingridientViewer, ICategoryViewer categoryViewer, IUnitOfWork unitOfWork, ICategoryController categoryController, ISubcategoryViewer subcategoryViewer, ISubcategoryController subcategoryController)
        {
            this.ingridientViewer = ingridientViewer;
            this.categoryViewer = categoryViewer;
            this.unitOfWork = unitOfWork;
            this.categoryController = categoryController;
            this.subcategoryViewer = subcategoryViewer;
            this.subcategoryController = subcategoryController;
        }
        string AddName()
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
        public List<string> AddRecipeSteps(List<string> recipeSteps)
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
        string AddDescription()
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
        //Метод для генерирования id-ков. В случае если есть в файле записи, то находим последний id-к
        int AddId(List<Recipe> modelRecipes)
        {
            if (modelRecipes.Count > 0)
            {
                //В файле не может быть повторяющихся id-ков
                return modelRecipes.Count + 1;
            }
            else
            {
                return 1;
            }
        }
        //Метод получения индексов рецептов, согласно выбранной подкатегории
        public List<int> GetIndicesBySubcategory(int subcategoryIndex)
        {
            //Объявляем переменную для сохранения индексов рецептов находящихся в выбранной категории
            List<int> selectIndexSubcategory = new List<int>();
            //Заносим индексы рецептов в лист, согласно выбранной подкатегории
            for (int i = 0; i < unitOfWork.Recipes.GetAll<Recipe>().ToList().Count; i++)
            {
                if (unitOfWork.Recipes.GetAll<Recipe>().ToList()[i].IdSubcategory == subcategoryIndex)
                {
                    selectIndexSubcategory.Add(i + 1);
                }
            }
            return selectIndexSubcategory;
        }
        //Метод получения индексов рецептов, согласно выбранной категории
        public List<int> GetIndicesByCategory(int categoryIndex)
        {
            //Объявляем переменную для сохранения индексов рецептов находящихся в выбранной категории
            List<int> selectIndexSubcategory = new List<int>();
            //Заносим индексы рецептов в лист, согласно выбранной подкатегории
            for (int i = 0; i < unitOfWork.Recipes.GetAll<Recipe>().ToList().Count; i++)
            {
                if (unitOfWork.Recipes.GetAll<Recipe>().ToList()[i].IdСategory == categoryIndex)
                {
                    selectIndexSubcategory.Add(i + 1);
                }
            }
            return selectIndexSubcategory;
        }
        //Метод для добавления рецептов. Рецепты добавляет пользователь на основании созданных категорий и ингридиентов
        public List<Recipe> CreateRecipe()
        {
            List<Recipe> listModelRecipes = new List<Recipe>();
            ConsoleKeyInfo keyPress;
            do
            {
                //Создаем экземпляр класса ModelRecipe
                Recipe modelRecipe = new Recipe();
                //Выводим на консоль название категорий
                categoryViewer.PrintingСategories(unitOfWork.Categories.GetAll<Category>().ToList());
                //Добавляем индекс категории
                modelRecipe.IdСategory = categoryController.CheckingCategoryIndex();
                //Выполняем проверку на наличие в данной категории - подкатегорий
                if (unitOfWork.Subcategories.GetAll<Subcategory>().ToList().Exists(x => x.IdCategory == modelRecipe.IdСategory))
                {
                    //Если подкатегории содержатся, то выводим их на печать
                    subcategoryViewer.PrintingSubcategories(unitOfWork.Subcategories.GetAll<Subcategory>().ToList(), modelRecipe.IdСategory);
                    modelRecipe.IdSubcategory = subcategoryController.CheckingSubcategoryIndex();
                }
                //Добавляем имя рецепту
                modelRecipe.Name = AddName();
                Console.WriteLine("\n\tВыбирте номер ингридиентa из списка:\n");
                //Механизм вывода списка ингредиентов на консоль
                ingridientViewer.PrintIngridient(unitOfWork.Ingredients.GetAll<Ingredient>().ToList());
                //Выделяем динамическую память под объект
                modelRecipe.IdIngredient = new List<int>();
                //Добавляем в лист с индексами указанных ингредиентов. Дубликаты не допускаются
                IngredientController ingredientController = new IngredientController();
                modelRecipe.IdIngredient.AddRange(ingredientController.FormationListIndices(unitOfWork.Ingredients.GetAll<Ingredient>().ToList()).Distinct().ToArray());
                //Добавляем описание рецепта
                modelRecipe.Description = AddDescription();
                //Выделяем динамическую память
                modelRecipe.Steps = new List<string>();
                //Добавляем в рецепт шаги приготовления рецепта. Повторение шагов не допускается
                modelRecipe.Steps.AddRange(AddRecipeSteps(modelRecipe.Steps).Distinct().ToArray());
                //Добавляем id-к
                modelRecipe.Id = AddId(unitOfWork.Recipes.GetAll<Recipe>().ToList());
                //Добавляем новый рецепт в переменную
                listModelRecipes.Add(modelRecipe);
                Console.WriteLine("\n\tДля введения следующего рецепта нажмите - 'Enter'" +
                                    "\n\tДля выхода в главное меню 'e'\n");
                keyPress = Console.ReadKey();

            } while (keyPress.KeyChar != 'e');
            return listModelRecipes;
        }
    }
}