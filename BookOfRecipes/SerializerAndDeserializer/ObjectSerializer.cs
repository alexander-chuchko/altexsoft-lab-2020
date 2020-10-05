using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;

namespace BookOfRecipes
{
    class ObjectSerializer<T>
    {
        public void SerializingFile(List<T> informationFile, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(informationFile, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }
    }
}