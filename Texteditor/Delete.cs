using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Texteditor
{
    class Delete
    {
        //Метод для изменения имени файла (добавления суффикса _copy)
        private static string Add_Path_Copy(string path)
        {
            string path_new_file;
            string add_copy = "_copy";
            path_new_file = @Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + add_copy + Path.GetExtension(path);
            return path_new_file;
        }
        //Метод для поиска и удаления слова/символа
        public static void FindAndDeleteWord(string path, string parametr)
        {

            //Создаем экземпляр класса FileInfo
            FileInfo fileInf = new FileInfo(path);
            //Проверяем существ. ли файл по указанному пути
            if (fileInf.Exists)
            {
                try
                {
                    //Выполняем копию файла с суффиксом "copy" по тому же пути с помощью метода Copy
                    File.Copy(path, Add_Path_Copy(path), true);
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
            string text = File.ReadAllText(path);
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
