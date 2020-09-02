using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using BookOfRecipes;

class ViewCategory
    {
        public static void PrintingСategories()
        {
            if (SaveList.categorySheet.Count > 0)
            {
                //Выводим имеющиеся категории
                for (int i = 0; i < SaveList.categorySheet.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", SaveList.categorySheet[i].id, SaveList.categorySheet[i].nameCategory);
                }
            }
            else
            {
                Console.WriteLine("В данном листе нет категорий");
            }

        }
    }

