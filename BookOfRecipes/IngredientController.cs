using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BookOfRecipes
{
    class IngredientController
    {
        //Метод для добавления захардкоженных ингридиентов 
        public static List<ModelIngredient> CreateIngredients()
        {
            string[] listIngredients = { "айва", "сахар","вода","лимона","молоко","сметана","яйцо","сливочное масло","ванильный сахар","соль","муки",
                                    "сухих дрожжи","малины","уксус яблочный","соевый соус","перец красный молотый","перец черный молотый",
                                    "жидкий мед","коричневый сахар","миндальных хлопьев","овсяных хлопьев","хурма","розмарин","орехи грецкие",
                                    "изюм","рис отварной","лаваш", "филе куринное","колбаса","перец болгарский сладкий","лук репчатый","помидор свежий",
                                    "чеснок","масло растительно","майонез","сосиски","плавленый сыр","горчица","кетчуп"};
            List<ModelIngredient> ingridient = new List<ModelIngredient>(listIngredients.Length);
            for (int i = 0; i < listIngredients.Length; i++)
            {
                ingridient.Add(new ModelIngredient { Id = i + 1, NameIngredient = listIngredients[i] });
            }
            return ingridient;
        }
        //Метод для формирования ингредиента
        public static ModelIngredient CreateIngredient(List<ModelIngredient> modelIngredients)
        {
            ModelIngredient modelIngredient=null;
            string userMessage = "Введите имя ингредиента: ";
            Console.WriteLine("\n\t{0}:", userMessage);
            string newIngredient = Console.ReadLine();
            if (!string.IsNullOrEmpty(newIngredient) && !modelIngredients.Exists(x => x.NameIngredient == newIngredient))
            {
                modelIngredient = new ModelIngredient() { Id = modelIngredients.Count + 1, NameIngredient = newIngredient };
                return modelIngredient;
            }
            else
            {
                Console.WriteLine("\n\tИмя ингредиента не введено!");
                return modelIngredient;
            }
        }
        //Метод для формирования списка выбранных индексов ингридиентов пользователем
        public static List<int> FormationListIndices(List<ModelIngredient> modelIngredients)
        {
            Console.WriteLine("\n\tНеобходимо указать номер ингридиента. По окончанию формирования списка введите - 'e'" +
                   "\n\tВведите номер:\n");
            List<int> ingredientIndices = new List<int>();
            for (int i = 0; i < modelIngredients.Count; i++)
            {
                string input = Console.ReadLine();
                //Выполняем проверку на корректность вводимого значения
                if (int.TryParse(input, out int result) && result <= modelIngredients.Count && result > 0)
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
}