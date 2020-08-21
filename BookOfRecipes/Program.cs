using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;

namespace BookOfRecipes
{
	
	[Serializable]
	class Recept
	{
		public string name {get; set;}
		public string script {get; set;}
		public List<string> step {get; set;}
        public List<int> idIngredient {get; set;}
		public int idCategoty {get; set;}
	}
	[Serializable]
	class Ingredient
	{
		public int id{get; set;}
		public string ingredient{get; set;}
	}
	
    [Serializable]
    class Category
    {
        public int id { get; set; }
        public string nameCategory { get; set; }
    }
    //Создаем класс для хранения глобальных листов    

    class SaveList
    {
    	public static List<Category>category {get; set;}
    	public static List<Recept>recept {get; set;}
    	public static List<Ingredient>ingredient {get; set;}
    }

    static class VerificationAndCreation
    {
        //Метод для проверки наличия файлов и в случае их отсутствия создать
        public static void FileChek()
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
  
                            SaveList.category =  DeserializingCategory.DeserializingCategoryFile(path);
                            break;
                        case "recept.json":
                            SaveList.recept = DeserializingRecept.DeserializingReceptFile(path);
                            break;
                        case "ingridient.json":
                            SaveList.ingredient = DeserializingIngredient.DeserializingIngredientFile(path);
                            break;
                    }
                }
                else
                {
                    switch (nameFile)
                    {
                        case "categoty.json":
                            SerializingCategory.SerializingCategoryFile(AddElementCategory.AddCategory(), path);
                            SaveList.category = AddElementCategory.AddCategory();
                            break;
                        case "recept.json":
                            SerializingRecept.SerializingReceptFile(AddElementRecept.AddRecept(), path);
                            SaveList.recept = AddElementRecept.AddRecept();
                            break;
                        case "ingridient.json":
                            SerializingIngredient.SerializingIngredientFile(AddElementIngredient.AddIngredient(), path);
                            SaveList.ingredient = AddElementIngredient.AddIngredient();
                            break;
                    }
                }
            }
        }
    }
    static class SerializingCategory
    {
        //Метод выполняющий сериализацию категорий
        public static void SerializingCategoryFile(List<Category> categories, string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(List<Category>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fs, categories);
            }
        }
    }
    static class SerializingRecept
    {
        //Метод выполняющий сериализацию рецептов
        public static void SerializingReceptFile(List<Recept> recept, string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(List<Recept>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fs, recept);
            }
        }
    }
    static class SerializingIngredient
    {
        //Метод выполняющий сериализацию ингридиентов
        public static void SerializingIngredientFile(List<Ingredient> ingridient, string path)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(List<Ingredient>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fs, ingridient);
            }
        }
    }

    static class DeserializingRecept
    {
        //Метод выполняющий десериализацию рецептов
        public static List<Recept> DeserializingReceptFile(string path)
        {
            DataContractSerializer dataContractSerialize = new DataContractSerializer(typeof(List<Recept>));
            List<Recept> recept = new List<Recept>();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                recept = (List<Recept>)dataContractSerialize.ReadObject(fs);
            }
            return recept;
        }
    }

    static class DeserializingIngredient
    {
        //Метод выполняющий десериализацию ингридиентов
        public static List<Ingredient> DeserializingIngredientFile(string path)
        {
            DataContractSerializer dataContractSerialize = new DataContractSerializer(typeof(List<Ingredient>));
            List<Ingredient> ingridient = new List<Ingredient>();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                ingridient = (List<Ingredient>)dataContractSerialize.ReadObject(fs);
            }
            return ingridient;
        }
    }
    static class DeserializingCategory
    {
        //Метод выполняющий десериализацию категорий
        public static List<Category> DeserializingCategoryFile(string path)
        {
            DataContractSerializer dataContractSerialize = new DataContractSerializer(typeof(List<Category>));
            List<Category> category = new List<Category>();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                category = (List<Category>)dataContractSerialize.ReadObject(fs);
            }
            return category;
        }
    }
    static class AddElementCategory
    {
        public static List<Category> AddCategory()
        {
            string[] nameCat = { "блюда из лаваша", "мясные блюда", "фруктово-ягодные блюда", "творожные блюда", "овощные блюда" };
            List<Category> categories = new List<Category>(nameCat.Length);
            for (int i = 0; i < nameCat.Length; i++)
            {
                categories.Add(new Category { id = i + 1, nameCategory = nameCat[i] });
            }
            return categories;
        }
    }
    static class AddElementIngredient
    {
        //Метод для добавления захардкоженных ингридиентов 
        public static List<Ingredient> AddIngredient()
        {
            string[] nameIngridient = { "айва", "сахар","вода","лимона","молоко","сметана","яйцо","сливочное масло","ванильный сахар","соль","муки",
                                    "сухих дрожжи","малины","уксус яблочный","соевый соус","перец красный молотый","перец черный молотый",
                                    "жидкий мед","коричневый сахар","миндальных хлопьев","овсяных хлопьев","хурма","розмарин","орехи грецкие",
                                    "изюм","рис отварной","лаваш", "филе куринное","колбаса","перец болгарский сладкий","лук репчатый","помидор свежий",
                                    "чеснок","масло растительно","майонез","сосиски","плавленый сыр","горчица","кетчуп"};
            List<Ingredient> ingridient = new List<Ingredient>(nameIngridient.Length);
            for (int i = 0; i < nameIngridient.Length; i++)
            {
                ingridient.Add(new Ingredient { id = i + 1, ingredient = nameIngridient[i] });
            }
            return ingridient;
        }
    }

    static class AddElementRecept
    {
        //Метод для добавления рецептов. Рецепты добавляет пользователь на основании созданных категорий и ингридиентов
        public static List<Recept> AddRecept()
        {
            List<Recept> recept = new List<Recept>();
            ConsoleKeyInfo keypress;
            do
            {
                Recept rec = new Recept();

                Console.WriteLine("\n\tВыбирите номер категории для рецепта");
                //Выводим имеющиеся категории
                for (int i = 0; i < SaveList.category.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", SaveList.category[i].id, SaveList.category[i].nameCategory);
                }
                //Выполняем проверку вводимого значения idCategoty
                if (Int32.TryParse(Console.ReadLine(), out int result))
                {
                    rec.idCategoty = result < SaveList.category.Count ? result : 0;
                }
                Console.WriteLine("\tВведите название рецепта:\n");
                string newRecept = Console.ReadLine();
                rec.name = newRecept != null ? newRecept : "Введите имя рецепта";
                //Выполняем проверку вводимого названия
                Console.WriteLine("\n\tВыбирте номер ингридиентa из списка:\n");
                //Механизм вывода списка ингридиентов на консоль
                for (int i = 0; i < SaveList.ingredient.Count; i++)
                {
                    Console.WriteLine("\n\t{0} - {1}", SaveList.ingredient[i].id, SaveList.ingredient[i].ingredient);
                }
                //Механизм для ввода индексов ингридиетов с которого состоит рецепт
                //Выделяем динамическую память под объект
                rec.idIngredient = new List<int>();
                Console.WriteLine("\n\tНеобходимо указать номер ингридиента. По окончанию формирования списка введите - 'exit'" +
                    "\n\tВведите номер:\n");
                for (int i = 0; i < SaveList.ingredient.Count; i++)
                {
                    string input = Console.ReadLine();
                    //Выполняем проверку на корректность вводимого значения
                    if (int.TryParse(input, out int res) && res <= SaveList.ingredient.Count && res > 0)
                    {
                        rec.idIngredient.Add(res);
                    }
                    else if (input == "exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n\tВведен неверный индекс ингридиента!");
                        i--;
                        continue;
                    }
                }
                Console.WriteLine("\n\tВведите описание рецепта:");
                string newScript = Console.ReadLine();
                rec.script = newScript != null ? newScript : "\n\tВведите описание рецепта";
                //Добавляем в рецепт шаги приготовления рецепта
                //Механизм для ввода индексов ингридиетов с которого состоит рецепт
                rec.step = new List<string>();
                Console.WriteLine("\n\tНеобходимо ввести шаги приготовления рецепта. По окончанию формирования списка шагов введите - 'exit'" +
                    "\n\tВведите шаги приготовления рецепта:\n");
                for (int i = 0; ; i++)
                {
                    Console.Write("\t" + i + ". ");
                    string inputStep = Console.ReadLine();
                    if (inputStep == "exit")
                    {
                        break;
                    }
                    if (inputStep != null)
                    {
                        rec.step.Add(inputStep);
                    }
                }
                recept.Add(rec);
                Console.WriteLine("\n\tДля введения следующего рецепта нажмите - 'Enter'" +
                                    "\n\tДля выхода нажмите клавишу 'E'\n");
                keypress = Console.ReadKey();
            } while (keypress.KeyChar != 'E');
            return recept;
        }
    }
    static class ShowCatalog
    {
        public static void Show()
        {
            ConsoleKeyInfo keypress;
            do
            {
                //Выводим список категорий
                Console.WriteLine("\tСписок категорий:\n");
                foreach (Category cat in SaveList.category)
                {
                    Console.WriteLine(string.Format("\t\t{0} - {1};", cat.id, cat.nameCategory));
                }
                Console.WriteLine("\n\tВыберите номер категорий:");
                string numberCategory = Console.ReadLine();
                if (int.TryParse(numberCategory, out int result) && SaveList.category.Count >= result && SaveList.category.Count > 0)
                {
                    Console.WriteLine(string.Format("\n\tВыбрана категория: {0}\n", SaveList.category[result - 1].nameCategory));
                    //Объявляем переменную для сохранения индексов отфильтрованных рецептов, согласно выбранной категории
                    List<int> selectIndex = new List<int>();
                    Console.WriteLine("\n\tВыводим имена рецептов:\n");
                    for (int i = 0; i < SaveList.recept.Count; i++)
                    {
                        if (SaveList.recept[i].idCategoty == result)
                        {
                            //Выводим имена рецептов
                            Console.WriteLine(string.Format("\n\t\t{0} - {1}\n\n", i + 1, SaveList.recept[i].name));
                            selectIndex.Add(i + 1);
                        }
                    }
                    Console.WriteLine(string.Format("\n\tЧтобы перейти на детали рецепта укажите номер:\n\n"));
                    string numberRecept = Console.ReadLine();

                    if (int.TryParse(numberRecept, out int res) && SaveList.recept.Count >= res && res > 0 && selectIndex.Contains(res))
                    {
                        Console.WriteLine(string.Format("\n\t\tНазвание: {0}\n\n\t\tОписание: {1}\n\n\t\tШаги:{2}\n", SaveList.recept[res - 1].name, SaveList.recept[res - 1].script, string.Join(" ", SaveList.recept[res - 1].step)));
                        Console.WriteLine("\n\t\tИнгридиенты: ");
                        //Выводим список ингридиентов выбранного пользователем рецепта
                        foreach (int i in SaveList.recept[res - 1].idIngredient)
                        {
                            Console.WriteLine("\t\t" + string.Join(" ", SaveList.ingredient[i].ingredient));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Введен некорректный номер рецепта!");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Введен некорректный номер категории!");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine("\n\tДля дальнейшего просмотра каталога рецептов нажмите - 'Enter'" +
                                  "\n\tДля создания нового рецепта нажмите клавишу 'N'" +
                                  "\n\tДля выхода из приложения нажмите клавишу - 'E'\n");
                keypress = Console.ReadKey();
                if (keypress.KeyChar == 'N')
                {
                    SaveList.recept.AddRange(AddElementRecept.AddRecept());
                    SerializingRecept.SerializingReceptFile(SaveList.recept, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\recept.json");
                    return;
                }
            } while (keypress.KeyChar != 'E');
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            VerificationAndCreation.FileChek();
            ShowCatalog.Show();

            Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}