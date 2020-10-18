using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    class CategoryViewer : ICategoryViewer
    {
        public void PrintingСategories(List<Category> modelCategories)
        {
            if (modelCategories.Count > 0)
            {
                //Выводим имеющиеся категории
                for (int i = 0; i < modelCategories.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", modelCategories[i].Id, modelCategories[i].Name);
                }
            }
            else
            {
                Console.WriteLine("В данном листе нет категорий");
            }
        }
    }
}