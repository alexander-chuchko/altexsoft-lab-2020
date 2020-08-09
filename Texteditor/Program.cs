#if true
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
    public static class OpenDirectory
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

    public static class Revers
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

    public static class Delete
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

    //Метод ItemWord
    public static class Counter 
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

    public static class PrintInformation
    {
        //Метод для получения информации о файле
        public static void File_info(string path)
        {
            FileInfo fileInf = new FileInfo(path);
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
#endif

