using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface IObjectSerializer
    {
        void SerializingFile(List<ISaveble> informationFile, string path);
    }
}
