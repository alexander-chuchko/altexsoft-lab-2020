using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BookOfRecipes
{
    class ContextEntity
    {
        public List<Category> CategorySheet { get; set; } = new List<Category>();
        public List<Recipe> RecipeSheet { get; set; } = new List<Recipe>();
        public List<Ingredient> IngredientSheet { get; set; } = new List<Ingredient>();
        public void SaveChanges()
        {
            string[] allFile = { "category.json", "ingredient.json", "recipe.json" };
            foreach (string nameFile in allFile)
            {
                string path =Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ "\\"+ nameFile);
                switch (nameFile)
                {
                    case "category.json":
                        ObjectSerializer<Category> objectSerializerCategory = new ObjectSerializer<Category>();
                        objectSerializerCategory.SerializingFile(CategorySheet, path);
                        break;
                    case "ingredient.json":
                        ObjectSerializer<Ingredient> objectSerializerdIngredient = new ObjectSerializer<Ingredient>();
                        objectSerializerdIngredient.SerializingFile(IngredientSheet, path);
                        break;
                    case "recipe.json":
                        ObjectSerializer<Recipe> objectSerializerdRecipe = new ObjectSerializer<Recipe>();
                        objectSerializerdRecipe.SerializingFile(RecipeSheet, path);
                        break;
                }
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}