using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BookOfRecipes
{
    class Catalog
    {
        private readonly ICategoryViewer categoryViewer;
        private readonly IRecipeViewer recipeViewer;
        private readonly IUnitOfWork unitOfWork;
        public Catalog(ICategoryViewer categoryViewer, IRecipeViewer recipeViewer, IUnitOfWork unitOfWork)
        {
            this.categoryViewer = categoryViewer;
            this.recipeViewer = recipeViewer;
            this.unitOfWork = unitOfWork;
        }

        //Метод выполняющий навигацию по каталогу
        public void ShowCatalog()
        {
            ConsoleKeyInfo keyPress;
            do
            {
                //Выводим список названий категорий
                categoryViewer.GetLink().PrintingСategories(unitOfWork.GetLink().GetCategory.GetAll().ToList());
                //Выполняем проверку корректности введенного индекса
                CategoryController categoryController = new CategoryController(unitOfWork.GetLink());
                int result = categoryController.CheckingCategoryIndex();
                //Проверка корректности  введенного индекса категории
                if (result != 0)
                {
                    Console.WriteLine(string.Format("\n\tВыбрана категория: {0}\n", unitOfWork.GetLink().GetCategory.GetAll().ToList()[result - 1].NameCategory));
                    //Выводим имена рецептов
                    if (recipeViewer.GetLink().PrintRecipesByСategory(result, unitOfWork.GetLink().GetRecipe.GetAll().ToList()) != 0)
                    {
                        //Объявляем переменную для сохранения индексов отфильтрованных рецептов, согласно выбранной категории
                        List<int> selectIndex = new List<int>();
                        //Заносим индексы рецептов согласно выбранной категории
                        for (int i = 0; i < unitOfWork.GetLink().GetRecipe.GetAll().ToList().Count; i++)
                        {
                            if (unitOfWork.GetLink().GetRecipe.GetAll().ToList()[i].IdСategory == result)
                            {
                                selectIndex.Add(i + 1);
                            }
                        }
                        //Просматриваем детали рецепт
                        recipeViewer.GetLink().PrintRecipeDetails(selectIndex, unitOfWork.GetLink().GetRecipe.GetAll().ToList(), unitOfWork.GetLink().GetIngredient.GetAll().ToList());

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