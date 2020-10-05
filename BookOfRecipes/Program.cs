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
            ContextEntity contextEntity = new ContextEntity();
            UnitOfWork unitOfWork = new UnitOfWork(contextEntity);
            CategoryViewer categoryViewer = new CategoryViewer();
            IngridientViewer ingridientViewer = new IngridientViewer();
            RecipeViewer recipeViewer = new RecipeViewer();
            CatalogFiles catalogFiles = new CatalogFiles(unitOfWork, categoryViewer, ingridientViewer);
            catalogFiles.HandlingFile();
            Navigation navigation = new Navigation(unitOfWork, categoryViewer, ingridientViewer, recipeViewer);
            navigation.ProvidingOptions();
            
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
