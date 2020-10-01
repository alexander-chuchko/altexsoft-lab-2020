using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    class ViewCategory
    {
        public void PrintingСategories(List<ModelCategory> modelCategories)
        {
            if (modelCategories.Count > 0)
            {
                //Выводим имеющиеся категории
                for (int i = 0; i < modelCategories.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", modelCategories[i].Id, modelCategories[i].NameCategory);
                }
            }
            else
            {
                Console.WriteLine("В данном листе нет категорий");
            }

        }
    }

}