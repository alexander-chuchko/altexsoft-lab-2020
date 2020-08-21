
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Texteditor
{
    class Program
    {
        static void Main(string[] args)
        {
            //название файла
            string namefile = "1.txt";
            //Захардкожен адрес по которому находиться файл
            string path = @System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + namefile;
            Console.WriteLine("\n\t\t\t\t\tApplication for working with text");
            Console.WriteLine("\n\tTo work with text, the application provides the following methods:");
            Console.WriteLine("\n\t1 - FindAndDeleteWord;\n\t2 - ItemWord;\n\t3 - ReverseWord;\n\t4 - GetDirectory;\n\t5 - File_info;\n\t");
            Console.WriteLine("\n\tTo start the application, specify the method index:");

            //Объявляем переменную
            ConsoleKeyInfo keypress;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    switch (number)
                    {
                        case 1:
                            Console.WriteLine("\n\tThe method named FindAndDeleteWord works\n\tSpecify parameter (character / word)/n");
                            Delete.FindAndDeleteWord(path, Console.ReadLine());
                            break;
                        case 2:
                            Console.WriteLine("\n\tThe method named ItemWord works\n");
                            Counter.ItemWord(path);
                            break;
                        case 3:
                            Console.WriteLine("\n\tThe method named ReverseWord works\n");
                            Revers.ReverseWord(path);
                            break;
                        case 4:
                            Console.WriteLine("\n\tThe method named GetDirectory works\n\tSpecify the path:");
                            OpenDirectory.GetDirectory(Console.ReadLine());
                            //Использовал для отладки
                            //GetDirectory(@"D:\EditFile\EditFile\EditFile");
                            break;
                        case 5:
                            Console.WriteLine("\n\tThe method named File_info works\n");
                            PrintInformation.File_info(path);
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


