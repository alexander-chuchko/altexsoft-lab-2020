using BookOfRecipes;
using System;
using System.Collections.Generic;
using System.Text;


    //Создаем класс для хранения глобальных листов    
    class SaveList
    {
        public static List<ModelCategory> categorySheet { get; set; }
        public static List<ModelRecipe> recipeSheet { get; set; }
        public static List<ModelIngredient> ingredientSheet { get; set; }
        public static string pathRecipe { get; set; }
    }

