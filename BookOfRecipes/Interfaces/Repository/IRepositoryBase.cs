using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes.Interfaces
{
    interface IRepositoryBase
    {
        IEnumerable<T> GetAll<T>() where T : ISaveble;
        void AddRange<T>(List<T> sheet) where T : ISaveble;
        void Add<T>(T entity) where T : ISaveble;
        void Delete<T>(T entity) where T : ISaveble;
        T GetById<T>(int id) where T : ISaveble;
    }
}
