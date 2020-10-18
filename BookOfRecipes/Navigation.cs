using BookOfRecipes.Interfaces;
using BookOfRecipes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    class Navigation
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryViewer categoryViewer;
        private readonly IIngridientViewer ingridientViewer;
        private readonly ICategoryController categoryController;
        private readonly IIngredientController ingredientController;
        private readonly IDirectoryViewer directoryViewer;
        private readonly IReceptController receptController;
        private readonly ISubcategoryController subcategoryController;
        private readonly ISubcategoryViewer subcategoryViewer;
        public Navigation(IUnitOfWork unitOfWork, ICategoryViewer categoryViewer, IIngridientViewer ingridientViewer, ICategoryController categoryController, IIngredientController ingredientController, IDirectoryViewer directoryViewer, IReceptController receptController, ISubcategoryViewer subcategoryViewer, ISubcategoryController subcategoryController)
        {
            this.unitOfWork = unitOfWork;
            this.categoryViewer = categoryViewer;
            this.ingridientViewer = ingridientViewer;
            this.categoryController = categoryController;
            this.ingredientController = ingredientController;
            this.directoryViewer = directoryViewer;
            this.receptController = receptController;
            this.subcategoryViewer = subcategoryViewer;
            this.subcategoryController = subcategoryController;
        }
        public int CheckValue(string value)
        {
            int numberOptions = 2;
            if (int.TryParse(value, out int result) && result > 0 && result <= numberOptions)
            {
                return result;
            }
            Console.WriteLine("\n\tВведен неверный индекс!\n");
            return 0;
        }

        public void ProvidingOptions()
        {
            string[] bookInterface =
                {
                "Просмотр каталога",
                "Создание рецепта",
                "Просмотр и создание категорий",
                "Просмотр и создание ингредиентов",
                "Просмотр и создание подкатегорий",
                "Сохранить данные",
                "Выход из приложения"
            };
            //int numberOfMethods = 7;
            Console.WriteLine("\n\tВ книге есть рецепты. Выберите необходимую опцию\n");
            for (int i = 0; i < bookInterface.Length; i++)
            {
                Console.WriteLine(string.Format("\n\t{0} - {1}\n", i + 1, bookInterface[i]));
            }
            //Console.WriteLine("\n\t1 - Просмотр каталога\n\t2 - Создание рецепта\n\t3 - Просмотр и создание категорий\n\t4 - Просмотр и создание ингредиентов\n\t5 - Просмотр и создание подкатегорий\n\t6 - Сохранить данные\n\t7 - Выход из приложения\n");
            Console.WriteLine("\n\tВведите необходимый индекс:");
            //Объявляем переменную
            ConsoleKeyInfo keyPress;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int number) && number > 0 && number <= bookInterface.Length)
                {
                    switch (number)
                    {

                        case 1:
                            Console.WriteLine("\n\tРаботает метод ShowCatalog\n");
                            directoryViewer.ShowCatalog();
                            Console.Clear();
                            ProvidingOptions();
                            break;
                        case 2:
                            Console.WriteLine("\n\tРаботает метод CreateRecipe\n");
                            unitOfWork.Recipes.AddRange(receptController.CreateRecipe());
                            Console.Clear();
                            ProvidingOptions();
                            break;
                        case 3:
                            do
                            {
                                Console.WriteLine("\n\tПросмотр и содание категорий\n");
                                Console.WriteLine("\n\t1 - Просмотр перечня категорий\n\t2 - Создать новую категорию\n");
                                int value = CheckValue(Console.ReadLine());
                                if (value > 0)
                                {
                                    switch (value)
                                    {
                                        case 1:
                                            //Выполняем просмотр перечня существующих категорий
                                            categoryViewer.PrintingСategories(unitOfWork.Categories.GetAll<Category>().ToList());
                                            break;
                                        case 2:
                                            //Создаем и добавляем новую категорию
                                            unitOfWork.Categories.Add(categoryController.CreateCategory(unitOfWork.Categories.GetAll<Category>().ToList()));
                                            break;
                                    }
                                }
                                Console.WriteLine("\n\tДля дальнейшего просмотра и содания категорий нажмите - 'Enter'" +
                                "\n\tДля выхода в главное меню - 'e'\n");
                                keyPress = Console.ReadKey();
                            } while (keyPress.KeyChar != 'e');
                            Console.Clear();
                            ProvidingOptions();
                            break;
                        case 4:
                            do
                            {
                                Console.WriteLine("\n\tПросмотр и содание ингредиентов \n");
                                Console.WriteLine("\n\t1 - Просмотр перечня ингредиентов\n\t2 - Создать новый ингредиент\n");
                                int value = CheckValue(Console.ReadLine());
                                if (value > 0)
                                {
                                    switch (value)
                                    {
                                        case 1:
                                            //Выполняем просмотр перечня существующих категорий
                                            ingridientViewer.PrintIngridient(unitOfWork.Ingredients.GetAll<Ingredient>().ToList());
                                            break;
                                        case 2:
                                            //Создаем и добавляем новый ингредиент
                                            unitOfWork.Ingredients.Add(ingredientController.CreateIngredient(unitOfWork.Ingredients.GetAll<Ingredient>().ToList()));
                                            break;
                                    }
                                }
                                Console.WriteLine("\n\tДля дальнейшего просмотра и содания ингредиентов нажмите - 'Enter'" +
                                "\n\tДля выхода в главное меню - 'e'\n");
                                keyPress = Console.ReadKey();
                            } while (keyPress.KeyChar != 'e');
                            Console.Clear();
                            ProvidingOptions();
                            break;
                        case 5:
                            do
                            {
                                Console.WriteLine("\n\tПросмотр и содание подкатегорий\n");
                                Console.WriteLine("\n\t1 - Просмотр перечня подкатегорий\n\t2 - Создать новую подкатегорию\n");
                                int value = CheckValue(Console.ReadLine());
                                if (value > 0)
                                {
                                    switch (value)
                                    {
                                        case 1:
                                            //Выполняем просмотр существующих подкатегорий в соответсвии с категориями
                                            subcategoryViewer.PrintingAllSubcategories(unitOfWork.Subcategories.GetAll<Subcategory>().ToList(), unitOfWork.Categories.GetAll<Category>().ToList());
                                            //categoryViewer.PrintingСategories(unitOfWork.Categories.GetAll<Category>().ToList());
                                            break;
                                        case 2:
                                            //Создаем и добавляем новую категорию
                                            unitOfWork.Subcategories.Add(subcategoryController.CreateSubcategory(unitOfWork.Subcategories.GetAll<Subcategory>().ToList()));
                                            //unitOfWork.Categories.Add(categoryController.CreateCategory(unitOfWork.Categories.GetAll<Category>().ToList()));
                                            break;
                                    }
                                }
                                Console.WriteLine("\n\tДля дальнейшего просмотра и содания подкатегорий нажмите - 'Enter'" +
                                "\n\tДля выхода в главное меню - 'e'\n");
                                keyPress = Console.ReadKey();
                            } while (keyPress.KeyChar != 'e');
                            Console.Clear();
                            ProvidingOptions();
                            break;
                        case 6:
                            Console.WriteLine("\n\tВыполняется сохранение данных\n");
                            unitOfWork.Commit();
                            Console.Clear();
                            ProvidingOptions();
                            break;
                        case 7:
                            Console.WriteLine("\n\tОсуществляется выход из программы!\n");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Введен неверный индекс!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Введен неверный индекс!");
                }
                Console.WriteLine("\n\tДля вызова следующего метода, нажмите 'Enter', для выхода из приложения нажмите клавишу 'e'\n");
                keyPress = Console.ReadKey();
            } while (keyPress.KeyChar != 'e');
        }
    }
}