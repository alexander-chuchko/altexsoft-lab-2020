using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Texteditor;

namespace TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Количество методов предоставленных пользователю
            int numberOfMethods = 5;
            Console.WriteLine("\n\t\t\t\t\tApplication for working with text");
            Console.WriteLine("\n\tTo work with text, the application provides the following methods:");
            Console.WriteLine("\n\t1 - FindAndDeleteWord;\n\t2 - ItemWord;\n\t3 - ReverseWord;\n\t4 - GetDirectory;\n\t5 - File_info;\n\t");
            Console.WriteLine("\n\tTo start the application, specify the method index:");

            //Объявляем переменную
            ConsoleKeyInfo keypress;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int number) && number > 0 && number <= numberOfMethods)
                {
                    switch (number)
                    {
                        case 1:
                            PathUser.ValidationValue();
                            Console.WriteLine("\n\tThe method named FindAndDeleteWord works\n\tSpecify parameter (character / word)\n");
                            FileOperation delete = new FileOperation(PathUser.path);
                            delete.FindAndDeleteWord(delete, Console.ReadLine());
                            break;
                        case 2:
                            PathUser.ValidationValue();
                            Console.WriteLine("\n\tThe method named ItemWord works\n");
                            Counter counter = new Counter(PathUser.path);
                            counter.ItemWord(counter);
                            break;
                        case 3:
                            PathUser.ValidationValue();
                            Console.WriteLine("\n\tThe method named ReverseWord works\n");
                            Revers revers = new Revers(PathUser.path);
                            revers.ReverseWord(revers);
                            break;
                        case 4:
                            Console.WriteLine("\n\tThe method named GetDirectory works\n\tSpecify the path:");
                            FolderStructure.GetDirectory(Console.ReadLine().Trim('"'));
                            break;
                        case 5:
                            PathUser.ValidationValue();
                            Console.WriteLine("\n\tThe method named File_info worksn\n\tSpecify the path: ");
                            InformationPrint printInformation = new InformationPrint(PathUser.path);
                            printInformation.File_info(printInformation);
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
            Console.ReadKey(true);
        }
    }
}
