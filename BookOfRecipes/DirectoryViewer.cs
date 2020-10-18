using BookOfRecipes.Interfaces;
using BookOfRecipes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BookOfRecipes
{
    class DirectoryViewer : IDirectoryViewer
    {
        private readonly ICategoryViewer categoryViewer;
        private readonly IRecipeViewer recipeViewer;
        private readonly ISubcategoryViewer subcategoryViewer;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryController categoryController;
        private readonly ISubcategoryController subcategoryController;
        private readonly IReceptController receptController;
        public DirectoryViewer(ICategoryViewer categoryViewer, IRecipeViewer recipeViewer, IUnitOfWork unitOfWork, ICategoryController categoryController, ISubcategoryViewer subcategoryViewer, ISubcategoryController subcategoryController, IReceptController receptController)
        {
            this.categoryViewer = categoryViewer;
            this.recipeViewer = recipeViewer;
            this.unitOfWork = unitOfWork;
            this.categoryController = categoryController;
            this.subcategoryViewer = subcategoryViewer;
            this.subcategoryController = subcategoryController;
            this.receptController = receptController;
        }
        //Метод выполняющий навигацию по каталогу
        public void ShowCatalog()
        {
            ConsoleKeyInfo keyPress;
            do
            {
                //Выводим список названий категорий
                categoryViewer.PrintingСategories(unitOfWork.Categories.GetAll<Category>().ToList());
                //Выполняем проверку корректности введенного индекса
                int resultCategory = categoryController.CheckingCategoryIndex();
                //Проверка корректности  введенного индекса категории
                if (resultCategory != 0)
                {
                    Console.WriteLine(string.Format("\n\tВыбрана категория: {0}\n", unitOfWork.Categories.GetAll<Category>().ToList()[resultCategory - 1].Name));
                    //Выпоняем проверку наличия подкатегорий в выбранной категории
                    if (unitOfWork.Subcategories.GetAll<Subcategory>().ToList().Exists(x => x.IdCategory == resultCategory))
                    {
                        //Если подкатегории содержатся, то выводим их на печать
                        subcategoryViewer.PrintingSubcategories(unitOfWork.Subcategories.GetAll<Subcategory>().ToList(), resultCategory);
                        int resultSubcategory = subcategoryController.CheckingSubcategoryIndex();
                        if (resultSubcategory != 0)
                        {
                            //Выводим имена рецептов в выбранной подкатегории
                            if (recipeViewer.PrintRecipesBySubсategory(resultSubcategory, unitOfWork.Recipes.GetAll<Recipe>().ToList()) != 0)
                            {
                                //Просматриваем детали рецепт
                                recipeViewer.PrintRecipeDetails(receptController.GetIndicesBySubcategory(resultSubcategory), unitOfWork.Recipes.GetAll<Recipe>().ToList(), unitOfWork.Ingredients.GetAll<Ingredient>().ToList());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Введен некорректный номер подкатегории!");
                            return;
                        }
                    }
                    else
                    {
                        //Выводим имена рецептов в выбранной категории
                        if (recipeViewer.PrintRecipesByСategory(resultCategory, unitOfWork.Recipes.GetAll<Recipe>().ToList()) != 0)
                        {
                            //Просматриваем детали рецепт
                            recipeViewer.PrintRecipeDetails(receptController.GetIndicesByCategory(resultCategory), unitOfWork.Recipes.GetAll<Recipe>().ToList(), unitOfWork.Ingredients.GetAll<Ingredient>().ToList());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Введен некорректный номер категории!");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine("\n\tДля дальнейшего просмотра каталога рецептов нажмите - 'Enter'" +
                                  "\n\tДля выхода в главное меню - 'e'\n");
                keyPress = Console.ReadKey();
            } while (keyPress.KeyChar != 'e');
        }
    }
}