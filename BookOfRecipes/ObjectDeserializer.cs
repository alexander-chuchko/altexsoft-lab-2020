using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;

namespace BookOfRecipes
{
    class ObjectDeserializer<T>
    {
        //Метод выполняющий десериализацию файлов
        public List<T> DeserializingFile(string path)
        {
            List<T> informationFile = new List<T>();
            informationFile = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
            return informationFile;
        }
    }
}