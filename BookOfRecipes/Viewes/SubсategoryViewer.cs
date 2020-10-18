using BookOfRecipes.Interfaces;
using BookOfRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes.Viewes
{
    class SubсategoryViewer : ISubcategoryViewer
    {
        public void PrintingSubcategories(List<Subcategory> modelSubcategories, int indexCategory)
        {
            if (modelSubcategories.Count > 0)
            {
                //Выводим имеющиеся подкатегории
                for (int i = 0; i < modelSubcategories.Count; i++)
                {
                    if(indexCategory==modelSubcategories[i].IdCategory)
                    {
                        Console.WriteLine("\n\t{0} - {1}", modelSubcategories[i].Id, modelSubcategories[i].Name);
                    }
                }
            }
            else
            {
                Console.WriteLine("В данной категории нет подкатегорий");
            }
        }
        public void PrintingAllSubcategories(List<Subcategory> subcategories, List<Category> categories)
        {
            if (subcategories.Count > 0)
            {
                for (int i=0; i<categories.Count; i++)
                {
                    if (subcategories.Exists(x => x.IdCategory == categories[i].Id))
                    {
                        Console.WriteLine(string.Format("\n\t{0} - {1}:",categories[i].Id, categories[i].Name));
                        int index = 1;
                        var allSubcategories = from entry in subcategories where entry.IdCategory == categories[i].Id select entry;
                        foreach(var subcategory in allSubcategories)
                        {
                            Console.WriteLine(string.Format("\n\t\t{0}.{1} - {2}", subcategory.IdCategory, index, subcategory.Name));
                            index++;
                        }
                        //Console.WriteLine(string.Format("\n\t\t - {0}", string.Join("\n\t\t - ", from entry in subcategories where entry.IdCategory == categories[i].Id select entry.Name))); 
                    }  
                }
            }
            else
            {
                Console.WriteLine("В книге нет подкатегорий");
            }
        }
    }
}
