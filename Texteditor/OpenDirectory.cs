using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texteditor
{
    class OpenDirectory
    {
        //Метод выполняющий вывод на консоль имена папок и файлов, а также позволяющий осуществлять навигацию по папкам ---выполнен--- 
        public static void GetDirectory(string path)
        {
            string[] astFolders = Directory.GetDirectories(path);
            Console.WriteLine("\n\tName folders: \n");
            //идентификатор для папок
            int counter = 0;
            DirectoryInfo dir = new DirectoryInfo(path);
            //проверяем наличие папок по указанному пути 
            if (dir.GetDirectories().Length > 0)
            {
                foreach (var item in dir.GetDirectories().OrderBy(f => f.Name))
                {
                    Console.WriteLine("\t -" + item.Name + "_" + counter);
                    counter++;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\tIn folder does not have folders!\n");
                return;
            }
            //Объявляем переменную
            ConsoleKeyInfo keypress;
            do
            {
                Console.WriteLine("\n\tSelect number is a folder:\n");
                //Выполняем провеку на ввод корректного значения (идентификатора папок) 
                if (Int32.TryParse(Console.ReadLine(), out int result))
                {
                    if (astFolders.Length >= result)
                    {
                        DirectoryInfo d1 = new DirectoryInfo(astFolders[result]);
                        FileInfo[] fi1 = d1.GetFiles();
                        if (fi1.Length > 0)
                        {
                            Console.WriteLine("\t\tSelect name file: \n");

                            foreach (FileInfo s in fi1.OrderBy(f => f.Name))
                            {
                                //Выводим на консоль список файлов в текущей папке
                                Console.WriteLine("\t" + s);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\tIn folder does not have files!\n");
                        }
                    }
                    Console.WriteLine("\n\tPress the key to continue browsing the contents of the folders 'Enter',\n to end viewing press the key 'E'\n");
                    keypress = Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\tInvalid value entered!\n");
                    //Выходим из метода
                    return;
                }
            } while (keypress.KeyChar != 'E');
        }
    }
}
