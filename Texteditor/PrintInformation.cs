using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texteditor
{
    class PrintInformation:FileOperation
    {
        public PrintInformation(string path)
        {
            this.path = path;
            this.fileInfo = new FileInfo(path);
        }
        //Метод для получения информации о файле
        public void File_info(PrintInformation obj)
        {
            //FileInfo fileInf = new FileInfo(path);
            //Выполняем проверку на наличие файла по указаному пути
            if (obj.ExistenceСheckFile())
            {
                //Console.WriteLine();
                Console.WriteLine("\tFile name: {0}", obj.fileInfo.Name);
                Console.WriteLine("\tTime of creation: {0}", obj.fileInfo.CreationTime);
                Console.WriteLine("\tSize: {0}", obj.fileInfo.Length);
                Console.WriteLine("\tBinary path: {0}", obj.fileInfo.FullName);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\n\tFile not found!\n");
            }
        }
    }
}
