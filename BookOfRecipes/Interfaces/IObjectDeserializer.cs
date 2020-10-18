using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface IObjectDeserializer
    {
        List<T> DeserializingFile<T>(string path);
    }
}
