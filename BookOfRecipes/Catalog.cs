using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using BookOfRecipes;

static class Catalog
    {
        //Метод выполняющий навигацию по каталогу
        public static void ShowCatalog()
        {
            ConsoleKeyInfo keypress;
            do
            {
                //Выводим список названий категорий
                ViewCategory.PrintingСategories();
                //Выполняем проверку корректности введенного индекса
                int result = CategoryController.CheckingCategoryIndex();
                //Проверка корректности  введенного индекса категории
                if (result != 0)
                {
                    Console.WriteLine(string.Format("\n\tВыбрана категория: {0}\n", SaveList.categorySheet[result - 1].nameCategory));
                    //Выводим имена рецептов
                    if (ViewRecipe.PrintRecipesByСategory(result) != 0)
                    {
                        //Объявляем переменную для сохранения индексов отфильтрованных рецептов, согласно выбранной категории
                        List<int> selectIndex = new List<int>();
                        //Заносим индексы рецептов согласно выбранной категории
                        for (int i = 0; i < SaveList.recipeSheet.Count; i++)
                        {
                            if (SaveList.recipeSheet[i].idCategoty == result)
                            {
                                selectIndex.Add(i + 1);
                            }
                        }
                        //Просматриваем детали рецепта
                        ViewRecipe.PrintRecipeDetails(selectIndex);
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
                keypress = Console.ReadKey();
            } while (keypress.KeyChar != 'e');
            //Navigation.ProvidingOptions();
        }
    }

