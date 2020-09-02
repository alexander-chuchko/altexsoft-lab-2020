using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;



    static class IngredientController
    {
        //Метод для добавления захардкоженных ингридиентов 
        public static List<ModelIngredient> CreateIngredient()
        {
            string[] nameIngridient = { "айва", "сахар","вода","лимона","молоко","сметана","яйцо","сливочное масло","ванильный сахар","соль","муки",
                                    "сухих дрожжи","малины","уксус яблочный","соевый соус","перец красный молотый","перец черный молотый",
                                    "жидкий мед","коричневый сахар","миндальных хлопьев","овсяных хлопьев","хурма","розмарин","орехи грецкие",
                                    "изюм","рис отварной","лаваш", "филе куринное","колбаса","перец болгарский сладкий","лук репчатый","помидор свежий",
                                    "чеснок","масло растительно","майонез","сосиски","плавленый сыр","горчица","кетчуп"};
            List<ModelIngredient> ingridient = new List<ModelIngredient>(nameIngridient.Length);
            for (int i = 0; i < nameIngridient.Length; i++)
            {
                ingridient.Add(new ModelIngredient { id = i + 1, nameIngredient = nameIngridient[i] });
            }
            return ingridient;
        }
        //Метод для формирования списка выбранных индексов ингридиентов пользователем
        public static List<int> FormationListIndices()
        {
            Console.WriteLine("\n\tНеобходимо указать номер ингридиента. По окончанию формирования списка введите - 'e'" +
                   "\n\tВведите номер:\n");
            List<int> ingredientIndices = new List<int>();
            for (int i = 0; i < SaveList.ingredientSheet.Count; i++)
            {
                string input = Console.ReadLine();
                //Выполняем проверку на корректность вводимого значения
                if (int.TryParse(input, out int result) && result <= SaveList.ingredientSheet.Count && result > 0)
                {
                    ingredientIndices.Add(result);
                }
                else if (input == "e")
                {
                    return ingredientIndices;
                }
                else
                {
                    Console.WriteLine("\n\tВведен неверный индекс ингридиента!");
                    i--;
                    continue;
                }
            }
            return ingredientIndices;
        }
    }

