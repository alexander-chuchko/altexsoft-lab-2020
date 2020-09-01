﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Texteditor
{
    class FileOperation : FileData
    {
        //Конструктор класса 
        public FileOperation(string path)
        {
            this.path = path;
            this.fileInfo = new FileInfo(path);
        }
        //Метод для изменения имени файла (добавления суффикса _copy)
        private static string AddPathCopy(string path)
        {
            string pathNewFile;
            string word = "_copy";
            pathNewFile = @Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + word + Path.GetExtension(path);
            return pathNewFile;
        }
        //Метод для поиска и удаления слова/символа
        public void FindAndDeleteWord(string parametr)
        {
            //Проверяем существ. ли файл по указанному пути
            if (IsExistenceFile())
            {
                try
                {
                    //Выполняем копию файла с суффиксом "copy" по тому же пути с помощью метода Copy
                    File.Copy(path, AddPathCopy(path), true);
                    Console.WriteLine("\tFile copy was successful!\n");
                    Console.WriteLine();
                }
                catch (IOException exc)
                {
                    Console.WriteLine("\tFile Copy Error" + exc.Message);
                    return;
                }
            }
            else
            {
                Console.WriteLine("\tFile not found!\n");
                //Выходим из метода
                return;
            }
            //Выполняем считывание файла в переменную
            string text = ReadFile(); //File.ReadAllText(path);
            //Выполняем проверку в параметр метода parametr передано слово или символ

            //const string pattern = "up";
            MatchCollection myMatches = Regex.Matches(text, parametr, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if (myMatches.Count > 0)
            {
                //Регистр букв при удалении игнорируем
                Regex regex = new Regex(parametr, RegexOptions.IgnoreCase);
                string newText = null;
                //Переключатель
                bool check = true;
                foreach (Match nextMatch in myMatches)
                {
                    if (check)
                    {
                        //Вместо удаленного символа/слова установим at (@) для читабельности при проверке текста
                        newText = regex.Replace(text, "@", myMatches.Count, nextMatch.Index);
                        File.WriteAllText(path, newText);
                        check = false;
                        Console.WriteLine("\n\t\tMatches in the text: " + myMatches.Count);
                    }
                    Console.WriteLine("\n\tRemoved word " + nextMatch.Value + " by index: " + nextMatch.Index);
                }
            }
            else
            {
                Console.WriteLine("The entered value is missing in the text!");
                return;
            }
        }
    }
}
