using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using BookOfRecipes;

class Navigation
{
    public static void ProvidingOptions()
    {
        int numberOfMethods = 3;
        Console.WriteLine("\n\tВ книге есть рецепты. Выберите необходимый метод\n");
        Console.WriteLine("\n\t1 - ShowCatalog;\n\t2 - CreateRecipe;\n\t3 - ExitApplication\n");
        Console.WriteLine("\n\tВведите необходимый индекс:");
        //Объявляем переменную
        ConsoleKeyInfo keypress;
        do
        {
            if (int.TryParse(Console.ReadLine(), out int number) && number > 0 && number <= numberOfMethods)
            {
                switch (number)
                {
                    case 1:
                        Console.WriteLine("\n\tРаботает метод ShowCatalog\n");
                        Catalog.ShowCatalog();
                        Navigation.ProvidingOptions();
                        break;
                    case 2:
                        Console.WriteLine("\n\tРаботает метод CreateRecipe\n");
                        SaveList.recipeSheet.AddRange(ReceptController.CreateRecipe());
                        Navigation.ProvidingOptions();
                        break;
                    case 3:
                        Console.WriteLine("\n\tОсуществляется выход их программы!\n");
                        //Перед выходом из программы сохраняем файл с рецептами
                        ObjectSerializer<ModelRecipe> receptDetails = new ObjectSerializer<ModelRecipe>();
                        receptDetails.SerializingFile(SaveList.recipeSheet, SaveList.pathRecipe);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid value specified!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid value specified!");
            }
            Console.WriteLine("\n\tTo call the next method, press 'Enter', to exit the application press the key 'e'\n");
            keypress = Console.ReadKey();
        } while (keypress.KeyChar != 'e');
    }
}
