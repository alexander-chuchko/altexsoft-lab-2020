using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using BookOfRecipes.Interfaces;

namespace BookOfRecipes
{
    class ObjectSerializer : IObjectSerializer
    {
        public void SerializingFile(List<ISaveble> informationFile, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(informationFile, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }
    }
}