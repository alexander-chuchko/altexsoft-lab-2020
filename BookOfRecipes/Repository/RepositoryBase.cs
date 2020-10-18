using BookOfRecipes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes.Repository
{
    class RepositoryBase: IRepositoryBase
    {
        private ContextEntity contextEntity;
        public RepositoryBase(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
        }
        public IEnumerable<T> GetAll<T>() where T : ISaveble
        {
            if (contextEntity._container.ContainsKey(typeof(T)))
            {
                return contextEntity._container[typeof(T)].Cast<T>();
            }
            return Enumerable.Empty<T>();
           // throw new Exception("Element not found");
        }
        public void AddRange<T>(List<T> sheet) where T : ISaveble
        {
            if (contextEntity._container.ContainsKey(typeof(T)))
            {
                contextEntity._container[typeof(T)].AddRange(sheet.Cast<ISaveble>().ToList());
                return;
            }
            contextEntity._container.Add(typeof(T), sheet.Cast<ISaveble>().ToList());
        }
        public void Add<T>(T entity) where T : ISaveble
        {
            if (contextEntity._container.ContainsKey(typeof(T)))
            {
                contextEntity._container[typeof(T)].Add(entity);
                return;
            }
            var tempList = new List<T>() { entity };
            contextEntity._container.Add(typeof(T), tempList.Cast<ISaveble>().ToList());
        }
        public void Delete<T>(T entity) where T : ISaveble
        {
            if (contextEntity._container.ContainsKey(typeof(T)))
            {
                contextEntity._container[typeof(T)].Remove(entity);
            }
            throw new Exception("Элемент не найден.");
        }
        public T GetById<T>(int id) where T : ISaveble
        {
            return (T)contextEntity._container[typeof(T)].ToList()[id];
        }
    }
}
