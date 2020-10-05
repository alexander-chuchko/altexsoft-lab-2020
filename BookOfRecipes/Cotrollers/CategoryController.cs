using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookOfRecipes
{
    class CategoryController
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        //Метод для добавления списка категорий
        public List<Category> CreateCategories()
        {
            string[] listСategories = { "блюда из лаваша", "мясные блюда", "фруктово-ягодные блюда", "творожные блюда", "овощные блюда" };
            List<Category> categories = new List<Category>(listСategories.Length);
            for (int i = 0; i < listСategories.Length; i++)
            {
                categories.Add(new Category { Id = i + 1, NameCategory = listСategories[i] });
            }
            return categories;
        }
        //Метод для добавления категории
        public Category CreateCategory(List<Category> modelCategories)
        {
            Category modelCategory = null;
            Console.WriteLine("\n\tВведите название категории:\n");
            string input = Console.ReadLine();
            if (!modelCategories.Exists(x => x.NameCategory == input && string.IsNullOrEmpty(input)))
            {
                modelCategory = new Category() { NameCategory = input, Id = modelCategories.Count };
            }
            return modelCategory;
        }

        //Метод получения выбранного индекса каталога пользователем
        public int CheckingCategoryIndex()
        {
            Console.WriteLine("\n\tВыбирите номер категории");
            for (int i = 0; i < unitOfWork.GetLink().GetCategory.GetAll().ToList().Count; i++)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && unitOfWork.GetLink().GetCategory.GetAll().ToList().Count >= result && unitOfWork.GetLink().GetCategory.GetAll().ToList().Count > 0)
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