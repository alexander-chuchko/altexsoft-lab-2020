﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texteditor
{
    class Information : FileData
    {
        public Information(string path)
        {
            this.path = path;
            this.fileInfo = new FileInfo(path);
        }
        //Метод для получения информации о файле
        public void GettingFileParameters()
        {
            //Выполняем проверку на наличие файла по указаному пути
            if (IsExistenceFile())
            {
                //Console.WriteLine();
                Console.WriteLine("\tFile name: {0}", fileInfo.Name);
                Console.WriteLine("\tTime of creation: {0}", fileInfo.CreationTime);
                Console.WriteLine("\tSize: {0}", fileInfo.Length);
                Console.WriteLine("\tBinary path: {0}", fileInfo.FullName);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\n\tFile not found!\n");
            }
        }
    }
}
