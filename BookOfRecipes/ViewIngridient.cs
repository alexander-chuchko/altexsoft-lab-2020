using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;



    class ViewIngridient
    {
        //Метод выводит список ингредиентов на консоль
        public static void PrintIngridient()
        {
            if (SaveList.ingredientSheet.Count > 0)
            {
                for (int i = 0; i < SaveList.ingredientSheet.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", SaveList.ingredientSheet[i].id, SaveList.ingredientSheet[i].nameIngredient);
                }
            }
            else
            {
                Console.WriteLine("В данном списке нет ингредиентов.");

            }

        }
    }

