using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    class Navigation
    {
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

        public void ProvidingOptions(UnitOfWork unitOfWork)
        {
            int numberOfMethods = 6;
            ViewCategory viewCategory = new ViewCategory();
            ViewRecipe viewRecipe = new ViewRecipe();
            ViewIngridient viewIngridient = new ViewIngridient();
            Catalog catalog = new Catalog(viewCategory,viewRecipe);
            ReceptController receptController = new ReceptController(viewIngridient, viewCategory);
            Console.WriteLine("\n\tВ книге есть рецепты. Выберите необходимую опцию\n");
            Console.WriteLine("\n\t1 - Просмотр каталога\n\t2 - Создание рецепта\n\t3 - Просмотр и создание категорий\n\t4 - Просмотр и создание ингредиентов\n\t5 - Сохранить данные\n\t6 - Выход из приложения\n");
            Console.WriteLine("\n\tВведите необходимый индекс:");
            //Объявляем переменную
            ConsoleKeyInfo keyPress;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int number) && number > 0 && number <= numberOfMethods)
                {
                    switch (number)
                    {

                        case 1:
                            Console.WriteLine("\n\tРаботает метод ShowCatalog\n");
                            catalog.ShowCatalog(unitOfWork);
                            Console.Clear();
                            ProvidingOptions(unitOfWork);
                            break;
                        case 2:
                            Console.WriteLine("\n\tРаботает метод CreateRecipe\n");
                           
                            unitOfWork.repositoryRecipe.AddRange(receptController.CreateRecipe(unitOfWork));
                            Console.Clear();
                            ProvidingOptions(unitOfWork);
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
                                            viewCategory.PrintingСategories((List<ModelCategory>)unitOfWork.repositoryCategory.GetAll());
                                            break;
                                        case 2:
                                            //Создаем и добавляем новую категорию
                                            CategoryController categoryController = new CategoryController();
                                            unitOfWork.repositoryCategory.Add(categoryController.CreateCategory(unitOfWork.contextEntity.CategorySheet));
                                            break;
                                    }
                                }
                                Console.WriteLine("\n\tДля дальнейшего просмотра и содания категорий нажмите - 'Enter'" +
                                "\n\tДля выхода в главное меню - 'e'\n");
                                keyPress = Console.ReadKey();
                            } while (keyPress.KeyChar != 'e');
                            Console.Clear();
                            ProvidingOptions(unitOfWork);
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
                                            viewIngridient.PrintIngridient((List<ModelIngredient>)unitOfWork.repositoryIngredient.GetAll());
                                            break;
                                        case 2:
                                            //Создаем и добавляем новую категорию
                                            unitOfWork.repositoryIngredient.Add(IngredientController.CreateIngredient(unitOfWork.contextEntity.IngredientSheet));
                                            break;
                                    }
                                }
                                Console.WriteLine("\n\tДля дальнейшего просмотра и содания категорий нажмите - 'Enter'" +
                                "\n\tДля выхода в главное меню - 'e'\n");
                                keyPress = Console.ReadKey();
                            } while (keyPress.KeyChar != 'e');
                            Console.Clear();
                            ProvidingOptions(unitOfWork);
                            break;
                        case 5:
                            Console.WriteLine("\n\tВыполняется сохранение данных\n");
                            unitOfWork.Commit();
                            Console.Clear();
                            ProvidingOptions(unitOfWork);
                            break;
                        case 6:
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