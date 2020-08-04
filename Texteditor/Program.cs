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
    class Program
    {
        //Метод для изменения имени файла (добавления суффикса _copy)
        public static string Add_Path_Copy(string path)
        {
            string path_new_file;
            string add_copy = "_copy";
            path_new_file = @Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + add_copy + Path.GetExtension(path);
            return path_new_file;
        }

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
                Console.WriteLine("\n\tFile this name is not!!!\n");
            }
        }

        //Метод для подсчитывания слов  ---Выполнен---
        public static void ItemWord(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            //Выполняем проверку на наличие файла по указаному пути
            if (fileInf.Exists)
            {
                //Артикль это служебное слово
                string text = File.ReadAllText(path);
                string[] separators = { ",", ".", "!", "?", ":", ";", " ", "\n", "\r", "(", ")", "\"" };
                //Формируем массив слов. Артикль это служебное слово
                string[] allstrings = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                //Переменная для подсчета слов 
                int wordcount = allstrings.Length;

                for (int i = 0; i < allstrings.Length; i++)
                {
                    //Выполняем проверку строк на содержание числовых значений. Цифровые значения, я не считаю словом
                    if (allstrings[i].All(char.IsDigit))
                    {
                        wordcount--;
                    }
                }
                int count = 0;
                Console.WriteLine("\n\tNumber of words in the text: " + wordcount);
                Console.WriteLine();
                for (int i = 0; i < allstrings.Length; i++)
                {
                    if (count % 10 == 0 && i != 0)
                    {
                        Console.WriteLine(allstrings[i - 1].ToString() + ", ");
                        count = 0;
                    }
                    count++;
                }
            }
            else
            {
                Console.WriteLine("\n\tFile this name is not!\n");
            }
        }

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
                string[] separators = { ".", "?", "!" };
                //Получаем массив с предложениями 
                string[] allstrings = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                //Получаем массив со словами с 3-го предложения
                string[] separ = { ",", "!", "?", ":", ";", " " };
                string[] sentence = allstrings[2].Split(separ, StringSplitOptions.RemoveEmptyEntries).ToArray();

                //Массив для занесения слов с измененным порядком 
                string[] output = new string[sentence.Length];
                //Выполняем изменение порядка букв в словах 3-го предложения
                for (int i = 0; i < sentence.Length; i++)
                {
                    output[i] = new string(sentence[i].ToCharArray().Reverse().ToArray());
                }
                //Выполняем изменение порядка слов в 3-м предложении
                for (int i = 0; i < output.Length; i++)
                {
                    //Выполняем замену слова, если в нем более 1-й буквы. В противном случае переходим к следующей итерации 
                    if (output[i].Length > 1)
                    {
                        allstrings[index] = allstrings[index].Replace(sentence[i], output[i]);
                    }
                    else
                    {
                        continue;
                    }
                }
                //Выводим на консоль 3-е предложение (с измененным порядком букв в словах)
                Console.WriteLine("\n\tReverse: " + allstrings[2].ToString());
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\tFile this name is not!\n");
            }
        }

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
                foreach (var item in dir.GetDirectories().OrderBy(f=>f.Name))
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

                            foreach (FileInfo s in fi1.OrderBy(f=>f.Name))
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
            if (parametr.Length > 1)
            {
                //Выполняем проверку на наличие переданного слова в тексте
                //Слова в тексте должны полностью соответствовать переданному слову в метод 
                if (Regex.IsMatch(text, "\\b" + parametr + "\\b"))
                {
                    //В коллекцию Dictionary добавляем сочетание символов
                    Dictionary<string, string> punctuation = new Dictionary<string, string>
                    {
                        {"  ",""}, {" , ", ", "}, {" . ", ". "}, {" .", "."}
                    };

                    //Производим удаление входящих слов
                    text = text.TrimStart();
                    //Удаляем лишние пробелы
                    text = Regex.Replace(text, "[ ]+", " ");

                    //форматируем текст
                    foreach (var pun in punctuation)
                    {
                        text = text.Replace(pun.Key, pun.Value);
                    }

                    //Перезаписываем файл после удаления слова/символа и форматирования текста
                    File.WriteAllText(path, text);
                }
                else//В случае, если слово не найдено в тексте
                {
                    Console.WriteLine("\tThere is no specified word in the text!\n");
                    return;
                }
            }
            else//если передан символ
            {
                if (parametr.Length != 0)
                {
                    char ch = char.Parse(parametr);
                    //Если переданный символ буква - true, то заходим в if
                    if (char.IsLetter(ch))
                    {
                        //Проверяем регистр символа
                        if (char.IsLower(ch))
                        {
                            //Выполняем удаление указанного символа в тексте
                            text = text.Replace(parametr, "");
                            //Удаляем лишние пробелы
                            text = Regex.Replace(text, "[ ]+", " ");
                            //Выполняем запись в файл
                            File.WriteAllText(path, text);
                            //Начинаем диалог с пользователем
                            Console.WriteLine("\tSymbol deleted " + parametr + "lowercase." +
                            "Delete symbol " + parametr + "uppercase? Yes - 1, No - 2\n");
                            string value = Console.ReadLine();
                            //Выполняем проверку корректности вводимого значения
                            if (int.TryParse(value, out int val) && val <= 2 && val >= 0)
                            {
                                switch (val)
                                {
                                    case 1:
                                        text = text.Replace(parametr.ToUpper(), "");
                                        //Удаляем лишние пробелы
                                        text = Regex.Replace(text, "[ ]+", " ");
                                        //Выполняем запись в файл
                                        File.WriteAllText(path, text);
                                        Console.WriteLine("\tDelete symbol " + parametr + "uppercase completed successfully. File saved\n");
                                        return;
                                    case 2:
                                        Console.WriteLine("\tDelete symbol " + parametr + "uppercase failed\n");
                                        return;
                                    default:
                                        Console.WriteLine("\tThe entered value is missing!\n");
                                        return;
                                }
                            }
                            else
                            {
                                Console.WriteLine("\tInvalid value entered\n");
                                return;
                            }
                        }
                        else//Значит регистр символа верхний
                        {
                            //Выполняем удаление символа (буквы) в верхнем регистре
                            text = text.Replace(parametr, "");
                            //Удаляем лишние пробелы
                            text = Regex.Replace(text, "[ ]+", " ");
                            //Выполняем запись в файл
                            File.WriteAllText(path, text);
                            Console.WriteLine("\tSymbol deleted" + parametr + "uppercase.\t" +
                            "Delete symbol" + parametr + " lowercase? Yes - 1, No - 2\n");
                            string value = Console.ReadLine();
                            //Выполняем проверку корректности вводимого значения
                            if (int.TryParse(value, out int val) && val <= 2 && val >= 0)
                            {
                                switch (val)
                                {
                                    case 1:
                                        text = text.Replace(parametr.ToLower(), "");
                                        //Удаляем лишние пробелы
                                        text = Regex.Replace(text, "[ ]+", " ");
                                        //Перезаписываем файл после удаления слова/символа и форматирования текста
                                        File.WriteAllText(path, text); ;
                                        Console.WriteLine("\tSymbol deleted " + parametr + "lowercase completed successfully. File saved\n");
                                        return;
                                    case 2:
                                        Console.WriteLine("\tSymbol deleted" + parametr + "lowercase failed. File saved\n");
                                        return;
                                    default:
                                        Console.WriteLine("\tThe entered value is missing!\n");
                                        return;
                                }
                            }
                        }
                    }
                    else
                    {
                        text = text.Replace(parametr, "");
                        //Удаляем лишние пробелы
                        text = Regex.Replace(text, "[ ]+", " ");
                        //Перезаписываем файл после удаления слова/символа и форматирования текста
                        File.WriteAllText(path, text);
                    }
                }
                else
                {
                    Console.WriteLine("\tThe text does not contain the specified character!\n");
                    return;
                }
            }
        }
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
                            FindAndDeleteWord(path, Console.ReadLine());
                            break;
                        case 2:
                            Console.WriteLine("\n\tThe method named ItemWord works\n");
                            ItemWord(path);
                            break;
                        case 3:
                            Console.WriteLine("\n\tThe method named ReverseWord works\n");
                            ReverseWord(path);
                            break;
                        case 4:
                            Console.WriteLine("\n\tThe method named GetDirectory works\n\tSpecify the path:");
                            GetDirectory(Console.ReadLine());
                            //Использовал для отладки
                            //GetDirectory(@"D:\EditFile\EditFile\EditFile");
                            break;
                        case 5:
                            Console.WriteLine("\n\tThe method named File_info works\n");
                            File_info(path);
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
