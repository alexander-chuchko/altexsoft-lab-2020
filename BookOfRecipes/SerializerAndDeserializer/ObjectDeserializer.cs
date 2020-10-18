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
    class ObjectDeserializer : IObjectDeserializer
    {
        List<T> IObjectDeserializer.DeserializingFile<T>(string path)
        {
            List<T> informationFile = new List<T>();
            informationFile = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
            return informationFile;
        }
    }
}