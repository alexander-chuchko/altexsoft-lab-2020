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
            DataContractSerializer dataContractSerialize = new DataContractSerializer(typeof(List<T>));
            List<T> informationFile = new List<T>();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                informationFile = (List<T>)dataContractSerialize.ReadObject(fs);
            }
            return informationFile;
        }
    }
}
