using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Texteditor
{
    class Revers
    {
        //Метод выполняющий реверсирование слов в 3-м предложении  ---Выполнен---
        public static void ReverseWord(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            //Выполняем проверку на наличие файла по указаному пути
            if (fileInf.Exists)
            {
                //Индекс 3-го предложения. Согласно условию задания  
                int index = 2;
                //Заносим в переменную text - текст прочитанный из файла 
                string text = File.ReadAllText(path);
                //Получаем массив с предложениями 
                string[] allStrings = text.Split(new string[] { ".", "?", "!" }, StringSplitOptions.RemoveEmptyEntries);
                //Получаем массив со словами с 3-го предложения
                //Создаем экземпляр класса Regex.
                Regex TitleRegex = new Regex(@"[^\'\w\s]", RegexOptions.IgnoreCase);
                string[] allWords = Regex.Split(TitleRegex.Replace(allStrings[index].Trim(), string.Empty), @"\s+");
                //Массив для занесения слов с измененным порядком 
                string[] output = new string[allWords.Length];
                //Выполняем изменение порядка букв в словах 3-го предложения
                for (int i = 0; i < allWords.Length; i++)
                {
                    output[i] = new string(allWords[i].ToCharArray().Reverse().ToArray());
                }
                //Выполняем изменение порядка слов в 3-м предложении
                for (int i = 0; i < output.Length; i++)
                {
                    //Выполняем замену слова, если в нем более 1-й буквы. В противном случае переходим к следующей итерации 
                    if (output[i].Length > 1)
                    {
                        allStrings[index] = allStrings[index].Replace(allWords[i], output[i]);
                    }
                    else
                    {
                        continue;
                    }
                }
                //Выводим на консоль 3-е предложение (с измененным порядком букв в словах)
                Console.WriteLine("\n\tReverse: " + allStrings[2].ToString());
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\tFile this name is not!\n");
            }
        }
    }
}
