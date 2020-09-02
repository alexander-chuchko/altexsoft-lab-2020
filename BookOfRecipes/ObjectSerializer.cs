using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;


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

