using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texteditor
{
    class PrintInformation
    {
        //Метод для получения информации о файле
        public static void File_info(string path)
        {
            File_info fileInf = new FileInfo(path);
            //Выполняем проверку на наличие файла по указаному пути
            if (fileInf.Exists)
            {
                //Console.WriteLine();
                Console.WriteLine("\tFile name: {0}", fileInf.Name);
                Console.WriteLine("\tTime of creation: {0}", fileInf.CreationTime);
                Console.WriteLine("\tSize: {0}", fileInf.Length);
                Console.WriteLine("\tBinary path: {0}", fileInf.FullName);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\n\tFile not found!\n");
            }
        }
    }
}
