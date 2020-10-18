using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BookOfRecipes
{
    class IngredientController : IIngredientController
    {
        //Метод для добавления захардкоженных ингредиентов 
        public List<Ingredient> CreateIngredients()
        {
            string[] listIngredients = { "айва", "сахар","вода","лимона","молоко","сметана","яйцо","сливочное масло","ванильный сахар","соль","муки",
                                    "сухих дрожжи","малины","уксус яблочный","соевый соус","перец красный молотый","перец черный молотый",
                                    "жидкий мед","коричневый сахар","миндальных хлопьев","овсяных хлопьев","хурма","розмарин","орехи грецкие",
                                    "изюм","рис отварной","лаваш", "филе куринное","колбаса","перец болгарский сладкий","лук репчатый","помидор свежий",
                                    "чеснок","масло растительно","майонез","сосиски","плавленый сыр","горчица","кетчуп"};
            List<Ingredient> ingridient = new List<Ingredient>(listIngredients.Length);
            for (int i = 0; i < listIngredients.Length; i++)
            {
                ingridient.Add(new Ingredient { Id = i + 1, Name = listIngredients[i] });
            }
            return ingridient;
        }
        //Метод для формирования ингредиента
        public Ingredient CreateIngredient(List<Ingredient> modelIngredients)
        {
            Ingredient modelIngredient = null;
            string userMessage = "Введите имя ингредиента: ";
            Console.WriteLine("\n\t{0}:", userMessage);
            string newIngredient = Console.ReadLine();
            if (!string.IsNullOrEmpty(newIngredient) && !modelIngredients.Exists(x => x.Name == newIngredient))
            {
                modelIngredient = new Ingredient() { Id = modelIngredients.Count + 1, Name = newIngredient };
                return modelIngredient;
            }
            else
            {
                Console.WriteLine("\n\tИмя ингредиента не введено!");
                return modelIngredient;
            }
        }
        //Метод для формирования списка выбранных индексов ингредиентов пользователем
        public List<int> FormationListIndices(List<Ingredient> modelIngredients)
        {
            Console.WriteLine("\n\tНеобходимо указать номер ингредиента. По окончанию формирования списка введите - 'e'" +
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
                    Console.WriteLine("\n\tВведен неверный индекс ингредиента!");
                    i--;
                    continue;
                }
            }
            return ingredientIndices;
        }
    }
}