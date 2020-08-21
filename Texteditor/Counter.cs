using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Texteditor
{
    class Counter
    {
        //Метод для подсчитывания слов  ---Выполнен---
        public static void ItemWord(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            //Выполняем проверку на наличие файла по указаному пути
            if (fileInf.Exists)
            {
                //Артикль это служебное слово
                string text = File.ReadAllText(path);
                //Создаем экземпляр класса Regex 
                Regex TitleRegex = new Regex(@"[^\'\w\s]", RegexOptions.IgnoreCase);
                //Создаем массив строк
                string[] allstrings = Regex.Split(TitleRegex.Replace(text, string.Empty), @"\s+");
                Console.WriteLine("\n\tNumber of words in the text: " + allstrings.Length + "\n");
                //Счетчик
                int count = 1;
                //Номер выводимого слова из текста
                int wordNumber = 10;
                string[] everyTenthWord = new string[(int)(allstrings.Length / wordNumber)];
                for (int i = 0, j = 0; i < allstrings.Length; i++)
                {
                    if (count % wordNumber == 0 && i != 0)
                    {
                        everyTenthWord[j] = allstrings[i];
                        count = 0;
                        j++;
                    }
                    count++;
                }
                Console.Write(" ");
                Console.WriteLine(string.Join(",\n ", everyTenthWord));
            }
            else
            {
                Console.WriteLine("\n\tFile not found!\n");
            }
        }
    }
}
