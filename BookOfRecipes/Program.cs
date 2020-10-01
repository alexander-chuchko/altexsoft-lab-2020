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
            CatalogFiles catalogFiles = new CatalogFiles();
            catalogFiles.HandlingFile(unitOfWork);
            Navigation navigation = new Navigation();
            navigation.ProvidingOptions(unitOfWork);

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
