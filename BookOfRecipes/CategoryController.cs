using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using BookOfRecipes;

static class CategoryController
    {
        public static List<ModelCategory> CreateCategories()
        {
            string[] nameCat = { "блюда из лаваша", "мясные блюда", "фруктово-ягодные блюда", "творожные блюда", "овощные блюда" };
            List<ModelCategory> categories = new List<ModelCategory>(nameCat.Length);
            for (int i = 0; i < nameCat.Length; i++)
            {
                categories.Add(new ModelCategory { id = i + 1, nameCategory = nameCat[i] });
            }
            return categories;
        }

        //Метод получения выбранного индекса каталога пользователем
        public static int CheckingCategoryIndex()
        {
            Console.WriteLine("\n\tВыбирите номер категории");
            for (int i = 0; i < SaveList.categorySheet.Count; i++)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && SaveList.categorySheet.Count >= result && SaveList.categorySheet.Count > 0)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("\n\tВведен некорректный номер категории!\n\tВыберите номер категрии\n");
                    i--;
                    continue;
                }
            }
            return 0;
        }
    }

