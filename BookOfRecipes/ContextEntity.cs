using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BookOfRecipes
{
    class ContextEntity
    {
        public List<ModelCategory> CategorySheet { get; set; } = new List<ModelCategory>();
        public List<ModelRecipe> RecipeSheet { get; set; } = new List<ModelRecipe>();
        public List<ModelIngredient> IngredientSheet { get; set; } = new List<ModelIngredient>();
        public void SaveChanges()
        {
            string[] allFile = { "categoty.json", "ingridient.json", "recept.json" };
            foreach (string nameFile in allFile)
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + nameFile;
                switch (nameFile)
                {
                    case "categoty.json":
                        ObjectSerializer<ModelCategory> objectSerializerCategory = new ObjectSerializer<ModelCategory>();
                        objectSerializerCategory.SerializingFile(CategorySheet, path);
                        break;
                    case "ingridient.json":
                        ObjectSerializer<ModelIngredient> objectSerializerdIngredient = new ObjectSerializer<ModelIngredient>();
                        objectSerializerdIngredient.SerializingFile(IngredientSheet, path);
                        break;
                    case "recept.json":
                        ObjectSerializer<ModelRecipe> objectSerializerdRecipe = new ObjectSerializer<ModelRecipe>();
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
