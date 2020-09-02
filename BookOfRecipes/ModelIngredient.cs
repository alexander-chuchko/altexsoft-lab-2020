using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Reflection;



    [Serializable]
    class ModelIngredient
    {
       public int id { get; set; }
       public string nameIngredient { get; set; }
    } 

