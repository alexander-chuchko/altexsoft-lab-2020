using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using BookOfRecipes;

namespace BookOfRecipes
{
   
    [Serializable]
    class ModelIngredient
    {
        public int id { get; set; }
        public string nameIngredient { get; set; }
    }

    [Serializable]
    class ModelCategory
    {
        public int id { get; set; }
        public string nameCategory { get; set; }
    }
    //Создаем класс для хранения глобальных листов    
    class SaveList
    {
        public static List<ModelCategory> categorySheet { get; set; }
        public static List<ModelRecipe> recipeSheet { get; set; }
        public static List<ModelIngredient> ingredientSheet { get; set; }
        public static string pathRecipe { get; set; }
    }

    static class VerificationAndCreation
    {
        //Метод для проверки наличия файлов и в случае их отсутствия создать
        public static void HandlingFile()
        {
            string[] allFile = { "categoty.json", "ingridient.json", "recept.json" };

            foreach (string nameFile in allFile)
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + nameFile;
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    //В случае если файлы есть с данным именем, то заносим их в глобальные переменные 
                    switch (nameFile)
                    {
                        case "categoty.json":
                            ObjectDeserializer<ModelCategory> categoryDetails = new ObjectDeserializer<ModelCategory>();
                            SaveList.categorySheet = categoryDetails.DeserializingFile(path);
                            break;
                        case "recept.json":
                            ObjectDeserializer<ModelRecipe> recipeDetails = new ObjectDeserializer<ModelRecipe>();
                            SaveList.recipeSheet = recipeDetails.DeserializingFile(path);
                            SaveList.pathRecipe = path;
                            break;
                        case "ingridient.json":
                            ObjectDeserializer<ModelIngredient> ingredientDetails = new ObjectDeserializer<ModelIngredient>();
                            SaveList.ingredientSheet = ingredientDetails.DeserializingFile(path);
                            break;
                    }
                }
                else
                {
                    switch (nameFile)
                    {
                        case "categoty.json":
                            SaveList.categorySheet = CategoryController.CreateCategories();
                            ObjectSerializer<ModelCategory> categoryDetails = new ObjectSerializer<ModelCategory>();
                            categoryDetails.SerializingFile(SaveList.categorySheet, path);
                            break;
                        case "recept.json":
                            Console.WriteLine("\n\tВ книге нет рецептов. Приступаем к созданию рецептов!\n");
                            SaveList.recipeSheet = ReceptController.CreateRecipe();
                            ObjectSerializer<ModelRecipe> receptDetails = new ObjectSerializer<ModelRecipe>();
                            receptDetails.SerializingFile(SaveList.recipeSheet, path);
                            SaveList.pathRecipe = path;
                            break;
                        case "ingridient.json":
                            SaveList.ingredientSheet = IngredientController.CreateIngredient();
                            ObjectSerializer<ModelIngredient> ingredientDetails = new ObjectSerializer<ModelIngredient>();
                            ingredientDetails.SerializingFile(SaveList.ingredientSheet, path);
                            break;
                    }
                }
            }
        }
    }

    static class CategoryController
    {
        public static List<ModelCategory> CreateCategories()
        {
            string[] nameCat = { "блюда из лаваша", "мясные блюда", "фруктово-ягодные блюда", "творожные блюда", "овощные блюда" };
            List<ModelCategory> categories = new List<ModelCategory>(nameCat.Length);
            for (int i = 0; i < nameCat.Length; i++)
            {
                categories.Add(new ModelCategory { id = i + 1, nameCategory = nameCat[i] });
            }
            return categories;
        }

        //Метод получения выбранного индекса каталога пользователем
        public static int CheckingCategoryIndex()
        {
            Console.WriteLine("\n\tВыбирите номер категории");
            for (int i = 0; i < SaveList.categorySheet.Count; i++)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && SaveList.categorySheet.Count >= result && SaveList.categorySheet.Count > 0)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("\n\tВведен некорректный номер категории!\n\tВыберите номер категрии\n");
                    i--;
                    continue;
                }
            }
            return 0;
        }
    }
    static class IngredientController
    {
        //Метод для добавления захардкоженных ингридиентов 
        public static List<ModelIngredient> CreateIngredient()
        {
            string[] nameIngridient = { "айва", "сахар","вода","лимона","молоко","сметана","яйцо","сливочное масло","ванильный сахар","соль","муки",
                                    "сухих дрожжи","малины","уксус яблочный","соевый соус","перец красный молотый","перец черный молотый",
                                    "жидкий мед","коричневый сахар","миндальных хлопьев","овсяных хлопьев","хурма","розмарин","орехи грецкие",
                                    "изюм","рис отварной","лаваш", "филе куринное","колбаса","перец болгарский сладкий","лук репчатый","помидор свежий",
                                    "чеснок","масло растительно","майонез","сосиски","плавленый сыр","горчица","кетчуп"};
            List<ModelIngredient> ingridient = new List<ModelIngredient>(nameIngridient.Length);
            for (int i = 0; i < nameIngridient.Length; i++)
            {
                ingridient.Add(new ModelIngredient { id = i + 1, nameIngredient = nameIngridient[i] });
            }
            return ingridient;
        }
        //Метод для формирования списка выбранных индексов ингридиентов пользователем
        public static List<int> FormationListIndices()
        {
            Console.WriteLine("\n\tНеобходимо указать номер ингридиента. По окончанию формирования списка введите - 'e'" +
                   "\n\tВведите номер:\n");
            List<int> ingredientIndices = new List<int>();
            for (int i = 0; i < SaveList.ingredientSheet.Count; i++)
            {
                string input = Console.ReadLine();
                //Выполняем проверку на корректность вводимого значения
                if (int.TryParse(input, out int result) && result <= SaveList.ingredientSheet.Count && result > 0)
                {
                    ingredientIndices.Add(result);
                }
                else if (input == "e")
                {
                    return ingredientIndices;
                }
                else
                {
                    Console.WriteLine("\n\tВведен неверный индекс ингридиента!");
                    i--;
                    continue;
                }
            }
            return ingredientIndices;
        }
    }
    class ViewIngridient
    {
        //Метод выводит список ингредиентов на консоль
        public static void PrintIngridient()
        {
            if (SaveList.ingredientSheet.Count > 0)
            {
                for (int i = 0; i < SaveList.ingredientSheet.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", SaveList.ingredientSheet[i].id, SaveList.ingredientSheet[i].nameIngredient);
                }
            }
            else
            {
                Console.WriteLine("В данном списке нет ингредиентов.");

            }

        }
    }

    class ViewCategory
    {
        public static void PrintingСategories()
        {
            if (SaveList.categorySheet.Count > 0)
            {
                //Выводим имеющиеся категории
                for (int i = 0; i < SaveList.categorySheet.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", SaveList.categorySheet[i].id, SaveList.categorySheet[i].nameCategory);
                }
            }
            else
            {
                Console.WriteLine("В данном листе нет категорий");
            }

        }
    }
    static class ReceptController
    {
        public static string AddName()
        {
            Console.WriteLine("\n\tВведите имя рецепта: \n");
            string userMessage = "Введите имя рецепта";
            for (; ; )
            {
                string inputName = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputName))
                {
                    return inputName;
                }
                else
                {
                    Console.WriteLine("\n\tВведите имя рецепта: \n");
                    return userMessage;
                }
            }
        }
        //Метод для формирования шагов приготовления рецепта 
        public static List<string> AddRecipeSteps(List<string> recipeSteps)
        {
            Console.WriteLine("\n\tНеобходимо ввести шаги приготовления рецепта. По окончанию формирования списка шагов введите - 'e'" +
                    "\n\tВведите шаги приготовления рецепта:\n");
            for (int i = 1; ; i++)
            {
                Console.Write("\t" + i + ". ");
                string inputStep = Console.ReadLine();
                if (inputStep == "e")
                {
                    return recipeSteps;
                }
                else if (!string.IsNullOrEmpty(inputStep))
                {
                    recipeSteps.Add(inputStep);
                }
                else
                {
                    Console.WriteLine("\n\tВведите шаг приготовления рецепта: \n");
                    continue;
                }
            }
        }
        //Метод для добавления формирования описания рецепта
        public static string AddDescription()
        {
            string userMessage = "Введите описание рецепта";
            Console.WriteLine("\n\t{0}:", userMessage);
            string newScript = Console.ReadLine();
            if (!string.IsNullOrEmpty(newScript))
            {
                return newScript;
            }
            else
            {
                Console.WriteLine("\n\tОписание рецепта не введено");
                return userMessage;
            }
        }

        //Метод для добавления рецептов. Рецепты добавляет пользователь на основании созданных категорий и ингридиентов
        public static List<ModelRecipe> CreateRecipe()
        {
            List<ModelRecipe> listModelRecipes = new List<ModelRecipe>();
            ConsoleKeyInfo keypress;
            do
            {
                //Environment.Exit(0);

                //Создаем экземпляр класса ModelRecipe
                ModelRecipe modelRecipe = new ModelRecipe();
                //Выводим на консоль название категорий
                ViewCategory.PrintingСategories();
                //Добавляем индекс категории
                modelRecipe.idCategoty = CategoryController.CheckingCategoryIndex();
                //Добавляем имя рецепту
                modelRecipe.nameRecept = ReceptController.AddName();
                Console.WriteLine("\n\tВыбирте номер ингридиентa из списка:\n");
                //Механизм вывода списка ингридиентов на консоль
                ViewIngridient.PrintIngridient();
                //Выделяем динамическую память под объект
                modelRecipe.idIngredient = new List<int>();
                //Добавляем в лист с индексами указанных ингредиентов. Дубликаты не допускаются
                modelRecipe.idIngredient.AddRange(IngredientController.FormationListIndices().Distinct().ToArray());
                //Добавляем описание рецепта
                modelRecipe.recipeDescription = ReceptController.AddDescription();
                //Выделяем динамическую память
                modelRecipe.recipeSteps = new List<string>();
                //Добавляем в рецепт шаги приготовления рецепта. Повторение шагов не допускается
                modelRecipe.recipeSteps.AddRange(ReceptController.AddRecipeSteps(modelRecipe.recipeSteps).Distinct().ToArray());
                //Добавляем новый рецепт в переменную
                listModelRecipes.Add(modelRecipe);
                Console.WriteLine("\n\tДля введения следующего рецепта нажмите - 'Enter'" +
                                    "\n\tДля выхода в главное меню 'e'\n");
                keypress = Console.ReadKey();
            } while (keypress.KeyChar != 'e');
            return listModelRecipes;
        }
    }

    class ViewRecipe
    {
        public static int PrintRecipesByСategory(int categoryNumber)
        {
            int counter = 0;
            Console.WriteLine("\n\tВыводим имена рецептов согласно указанной категории:\n");
            for (int i = 0; i < SaveList.recipeSheet.Count; i++)
            {
                if (SaveList.recipeSheet[i].idCategoty == categoryNumber)
                {
                    //Выводим имена рецептов
                    Console.WriteLine(string.Format("\n\t\t{0} - {1}\n\n", i + 1, SaveList.recipeSheet[i].nameRecept));
                    counter++;
                }
            }
            Console.WriteLine("\n\tВ данном списке {0} рецептов.\n", counter);
            return counter;
        }
        public static void PrintRecipeDetails(List<int> idRecipes)
        {
            if (SaveList.recipeSheet.Count > 0)
            {
                Console.WriteLine(string.Format("\n\tЧтобы перейти на детали рецепта укажите номер:\n\n"));
                string numberRecept = Console.ReadLine();

                if (int.TryParse(numberRecept, out int result) && SaveList.recipeSheet.Count >= result && result > 0 && idRecipes.Contains(result))
                {
                    Console.WriteLine(string.Format("\n\t\tНазвание: {0}\n\n\t\tОписание: {1}\n\n\t\tШаги:{2}\n", SaveList.recipeSheet[result - 1].nameRecept, SaveList.recipeSheet[result - 1].recipeDescription, string.Join(" ", SaveList.recipeSheet[result - 1].recipeSteps)));
                    Console.WriteLine("\n\t\tИнгридиенты: ");
                    //Выводим список ингридиентов выбранного пользователем рецепта
                    foreach (int i in SaveList.recipeSheet[result - 1].idIngredient)
                    {
                        Console.WriteLine("\t\t" + string.Join(" ", SaveList.ingredientSheet[i].nameIngredient));
                    }
                }
                else
                {
                    Console.WriteLine("Введен некорректный номер рецепта!");
                    return;
                }
            }
        }
    }
    static class Catalog
    {
        //Метод выполняющий навигацию по каталогу
        public static void ShowCatalog()
        {
            ConsoleKeyInfo keypress;
            do
            {
                //Выводим список названий категорий
                ViewCategory.PrintingСategories();
                //Выполняем проверку корректности введенного индекса
                int result = CategoryController.CheckingCategoryIndex();
                //Проверка корректности  введенного индекса категории
                if (result != 0)
                {
                    Console.WriteLine(string.Format("\n\tВыбрана категория: {0}\n", SaveList.categorySheet[result - 1].nameCategory));
                    //Выводим имена рецептов
                    if (ViewRecipe.PrintRecipesByСategory(result) != 0)
                    {
                        //Объявляем переменную для сохранения индексов отфильтрованных рецептов, согласно выбранной категории
                        List<int> selectIndex = new List<int>();
                        //Заносим индексы рецептов согласно выбранной категории
                        for (int i = 0; i < SaveList.recipeSheet.Count; i++)
                        {
                            if (SaveList.recipeSheet[i].idCategoty == result)
                            {
                                selectIndex.Add(i + 1);
                            }
                        }
                        //Просматриваем детали рецепта
                        ViewRecipe.PrintRecipeDetails(selectIndex);
                    }
                }
                else
                {
                    Console.WriteLine("Введен некорректный номер категории!");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine("\n\tДля дальнейшего просмотра каталога рецептов нажмите - 'Enter'" +
                                  "\n\tДля выхода в главное меню - 'e'\n");
                keypress = Console.ReadKey();
            } while (keypress.KeyChar != 'e');
            //Navigation.ProvidingOptions();
        }
    }
    class ObjectDeserializer<T>
    {
        //Метод выполняющий десериализацию файлов
        public List<T> DeserializingFile(string path)
        {
            DataContractSerializer dataContractSerialize = new DataContractSerializer(typeof(List<T>));
            List<T> informationFile = new List<T>();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                informationFile = (List<T>)dataContractSerialize.ReadObject(fs);
            }
            return informationFile;
        }
    }

    class ObjectSerializer<T>
    {
        //Метод выполняющий сериализацию объектов в json файл
        public void SerializingFile(List<T> informationFile, string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fs, informationFile);
            }
        }
    }

    class Navigation
    {
        public static void ProvidingOptions()
        {
            int numberOfMethods = 3;
            Console.WriteLine("\n\tВ книге есть рецепты. Выберите необходимый метод\n");
            Console.WriteLine("\n\t1 - ShowCatalog;\n\t2 - CreateRecipe;\n\t3 - ExitApplication\n");
            Console.WriteLine("\n\tВведите необходимый индекс:");
            //Объявляем переменную
            ConsoleKeyInfo keypress;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int number) && number > 0 && number <= numberOfMethods)
                {
                    switch (number)
                    {
                        case 1:
                            Console.WriteLine("\n\tРаботает метод ShowCatalog\n");
                            Catalog.ShowCatalog();
                            Navigation.ProvidingOptions();
                            break;
                        case 2:
                            Console.WriteLine("\n\tРаботает метод CreateRecipe\n");
                            SaveList.recipeSheet.AddRange(ReceptController.CreateRecipe());
                            Navigation.ProvidingOptions();
                            break;
                        case 3:
                            Console.WriteLine("\n\tОсуществляется выход их программы!\n");
                            //Перед выходом из программы сохраняем файл с рецептами
                            ObjectSerializer<ModelRecipe> receptDetails = new ObjectSerializer<ModelRecipe>();
                            receptDetails.SerializingFile(SaveList.recipeSheet, SaveList.pathRecipe);
                            Environment.Exit(0);
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
        }
    }
    class Program
    {

        public static void Main(string[] args)
        {
            VerificationAndCreation.HandlingFile();
            Navigation.ProvidingOptions();
            //Catalog.ShowCatalog();
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
