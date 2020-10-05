using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    class ReceptController
    {
        private readonly ICategoryViewer categoryViewer;
        private readonly IIngridientViewer ingridientViewer;
        private readonly IUnitOfWork unitOfWork;
        public ReceptController(IIngridientViewer ingridientViewer, ICategoryViewer categoryViewer, IUnitOfWork unitOfWork)
        {
            this.ingridientViewer=ingridientViewer;
            this.categoryViewer=categoryViewer;
            this.unitOfWork = unitOfWork;
        }
        string AddName()
        {
            Console.WriteLine("\n\tВведите имя рецепта: \n");
            string userMessage = "Введите имя рецепта";
            for (; ;)
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
                categoryViewer.GetLink().PrintingСategories(unitOfWork.GetLink().GetCategory.GetAll().ToList());
                //Добавляем индекс категории
                CategoryController categoryController = new CategoryController(unitOfWork);
                modelRecipe.IdСategory = categoryController.CheckingCategoryIndex();
                //Добавляем имя рецепту
                modelRecipe.NameRecept = AddName();
                Console.WriteLine("\n\tВыбирте номер ингридиентa из списка:\n");
                //Механизм вывода списка ингредиентов на консоль
                ingridientViewer.GetLink().PrintIngridient(unitOfWork.GetLink().GetIngredient.GetAll().ToList());
                //Выделяем динамическую память под объект
                modelRecipe.IdIngredient = new List<int>();
                //Добавляем в лист с индексами указанных ингредиентов. Дубликаты не допускаются
                IngredientController ingredientController = new IngredientController();
                modelRecipe.IdIngredient.AddRange(ingredientController.FormationListIndices(unitOfWork.GetLink().GetIngredient.GetAll().ToList()).Distinct().ToArray());
                //Добавляем описание рецепта
                modelRecipe.RecipeDescription = AddDescription();
                //Выделяем динамическую память
                modelRecipe.RecipeSteps = new List<string>();
                //Добавляем в рецепт шаги приготовления рецепта. Повторение шагов не допускается
                modelRecipe.RecipeSteps.AddRange(AddRecipeSteps(modelRecipe.RecipeSteps).Distinct().ToArray());
                //Добавляем id-к
                modelRecipe.Id = AddId(unitOfWork.GetLink().GetRecipe.GetAll().ToList());
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