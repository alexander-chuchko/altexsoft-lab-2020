﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texteditor
{
    //Создаем базовый класс в который заносим повторяющийся код. Согласно замечаний
    public abstract class FileData
    {
        protected FileInfo fileInfo { get; set; }
        protected string path { get; set; }

        //Метод для проверки существования файлов
        public bool IsExistenceFile()
        {
            return fileInfo.Exists;
        }
        //Метод для чтения из файла
        public string ReadFile()
        {
            return File.ReadAllText(path).Length > 0 ? File.ReadAllText(path) : "File is empty!";
        }
    }
}

