using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    class CategoryController
    {
        //Метод для добавления списка категорий
        public List<ModelCategory> CreateCategories()
        {
            string[] listСategories = { "блюда из лаваша", "мясные блюда", "фруктово-ягодные блюда", "творожные блюда", "овощные блюда" };
            List<ModelCategory> categories = new List<ModelCategory>(listСategories.Length);
            for (int i = 0; i < listСategories.Length; i++)
            {
                categories.Add(new ModelCategory { Id = i + 1, NameCategory = listСategories[i] });
            }
            return categories;
        }
        //Метод для добавления категории
        public ModelCategory CreateCategory(List<ModelCategory> modelCategories)
        {
            ModelCategory modelCategory = null;
            Console.WriteLine("\n\tВведите название категории:\n");
            string input = Console.ReadLine();
            if (!modelCategories.Exists(x => x.NameCategory == input && string.IsNullOrEmpty(input)))
            {
                modelCategory = new ModelCategory() { NameCategory = input, Id = modelCategories.Count };
            }
            return modelCategory;
        }

        //Метод получения выбранного индекса каталога пользователем
        public int CheckingCategoryIndex(UnitOfWork unitOfWork)
        {
            Console.WriteLine("\n\tВыбирите номер категории");
            for (int i = 0; i < unitOfWork.contextEntity.CategorySheet.Count; i++)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && unitOfWork.contextEntity.CategorySheet.Count >= result && unitOfWork.contextEntity.CategorySheet.Count > 0)
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
}