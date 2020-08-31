using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texteditor
{
    class PathUser
    {
        //Для хранения адреса введенного пользователем
        public static string path;
        //Выполняем проверку в случае, если пользовтаель захотел использовать следующие методы
        public static void ValidationValue()
        {
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("\tEnter file address:\n");
                path = Console.ReadLine().Trim('"');
            }
            else
            {
                return;
            }
        }
    }
}
