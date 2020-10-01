﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BookOfRecipes
{
    class Catalog
    {
        //Метод выполняющий навигацию по каталогу
        public static void ShowCatalog(UnitOfWork unitOfWork)
        {
            ConsoleKeyInfo keyPress;
            do
            {
                //Выводим список названий категорий
                ViewCategory.PrintingСategories(unitOfWork.contextEntity.CategorySheet);
                //Выполняем проверку корректности введенного индекса
                int result = CategoryController.CheckingCategoryIndex(unitOfWork);
                //Проверка корректности  введенного индекса категории
                if (result != 0)
                {
                    Console.WriteLine(string.Format("\n\tВыбрана категория: {0}\n", unitOfWork.contextEntity.CategorySheet[result - 1].NameCategory));
                    //Выводим имена рецептов
                    if (ViewRecipe.PrintRecipesByСategory(result, unitOfWork.contextEntity.RecipeSheet) != 0)
                    {
                        //Объявляем переменную для сохранения индексов отфильтрованных рецептов, согласно выбранной категории
                        List<int> selectIndex = new List<int>();
                        //Заносим индексы рецептов согласно выбранной категории
                        for (int i = 0; i < unitOfWork.contextEntity.RecipeSheet.Count; i++)
                        {
                            if (unitOfWork.contextEntity.RecipeSheet[i].IdСategory == result)
                            {
                                selectIndex.Add(i + 1);
                            }
                        }
                        //Просматриваем детали рецепта
                        ViewRecipe.PrintRecipeDetails(selectIndex, unitOfWork.contextEntity.RecipeSheet, unitOfWork.contextEntity.IngredientSheet);
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