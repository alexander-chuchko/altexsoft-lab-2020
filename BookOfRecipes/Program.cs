using BookOfRecipes.Cotrollers;
using BookOfRecipes.Interfaces;
using BookOfRecipes.Viewes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace BookOfRecipes
{
    class Program
    {
        public static void Main(string[] args)
        {
            ObjectSerializer objectSerializer = new ObjectSerializer();
            ObjectDeserializer objectDeserializer = new ObjectDeserializer();

            ContextEntity contextEntity = new ContextEntity(objectSerializer);
            UnitOfWork unitOfWork = new UnitOfWork(contextEntity);

            CategoryViewer categoryViewer = new CategoryViewer();
            IngridientViewer ingridientViewer = new IngridientViewer();
            RecipeViewer recipeViewer = new RecipeViewer();
            SubсategoryViewer subсategoryViewer = new SubсategoryViewer();

            IngredientController ingredientController = new IngredientController();
            CategoryController categoryController = new CategoryController(unitOfWork);
            SubcategoryController subcategoryController = new SubcategoryController(unitOfWork, categoryController, categoryViewer);
            ReceptController receptController = new ReceptController(ingridientViewer, categoryViewer, unitOfWork, categoryController, subсategoryViewer, subcategoryController);

            DirectoryViewer directoryViewer = new DirectoryViewer(categoryViewer, recipeViewer, unitOfWork, categoryController, subсategoryViewer, subcategoryController, receptController);
            FileHandler catalogFiles = new FileHandler(unitOfWork, categoryController, receptController, ingredientController, objectDeserializer, subcategoryController);

            catalogFiles.UploadingOrCreatingFiles();

            Navigation navigation = new Navigation(unitOfWork, categoryViewer, ingridientViewer, categoryController,
                ingredientController, directoryViewer, receptController, subсategoryViewer, subcategoryController);
            navigation.ProvidingOptions();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
