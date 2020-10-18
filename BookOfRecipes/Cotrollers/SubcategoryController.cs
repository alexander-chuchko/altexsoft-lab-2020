using BookOfRecipes.Interfaces;
using BookOfRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes.Cotrollers
{
    class SubcategoryController : ISubcategoryController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryController categoryController;
        private readonly ICategoryViewer categoryViewer;
        public SubcategoryController(IUnitOfWork unitOfWork, ICategoryController categoryController, ICategoryViewer categoryViewer)
        {
            this.unitOfWork = unitOfWork;
            this.categoryController = categoryController;
            this.categoryViewer = categoryViewer;
        }
        //Метод для добавления списка подкатегорий
        public List<Subcategory> CreateSubcategories()
        {
            string[] listSubcategories = { "Блюда из айвы", "Блюда из малины", "Блюда их хурмы" };
            List<Subcategory> subcategories = new List<Subcategory>(listSubcategories.Length);
            for (int i = 0; i < listSubcategories.Length; i++)
            {
                subcategories.Add(new Subcategory { Id = i + 1, Name = listSubcategories[i], IdCategory = 3 });
            }
            return subcategories;
        }

        //Метод для добавления категории
        public Subcategory CreateSubcategory(List<Subcategory> modelSubcategories)
        {
            Subcategory modelSubCategory = null;
            //Выводим список названий категорий
            categoryViewer.PrintingСategories(unitOfWork.Categories.GetAll<Category>().ToList());
            Console.WriteLine("\n\tВведите индекс категории в которой необходимо создать подкатегорию\n");
            //Выполняем проверку корректности введенного индекса
            int resultCategory = categoryController.CheckingCategoryIndex();
            Console.WriteLine("\n\tВведите название подкатегории:\n");
            string input = Console.ReadLine();
            if (!modelSubcategories.Exists(x => x.Name == input && string.IsNullOrEmpty(input)))
            {
                modelSubCategory = new Subcategory() { Name = input, Id = modelSubcategories.Count + 1, IdCategory = resultCategory };
            }
            return modelSubCategory;
        }
        //Метод получения выбранного индекса подкатегории пользователем
        public int CheckingSubcategoryIndex()
        {
            Console.WriteLine("\n\tВыбирите номер подкатегории");
            for (int i = 0; i < unitOfWork.Categories.GetAll<Subcategory>().ToList().Count; i++)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && unitOfWork.Categories.GetAll<Subcategory>().ToList().Count >= result && unitOfWork.Categories.GetAll<Subcategory>().ToList().Count > 0)
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

