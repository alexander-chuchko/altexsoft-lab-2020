using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BookOfRecipes
{
    class ContextEntity
    {
        public Dictionary<Type, List<ISaveble>> _container { get; set; } = new Dictionary<Type, List<ISaveble>>();
        private readonly IObjectSerializer objectSerializer;
        public ContextEntity(IObjectSerializer objectSerializer)
        {
            this.objectSerializer = objectSerializer;
        }
        public void SaveChanges()
        {
            foreach (var item in _container)
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + item.Key.Name + ".json");
                objectSerializer.SerializingFile(item.Value, path);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}