using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);
        void AddRange(IEnumerable<T> entities);
        IEnumerable<T> GetAll();
    }
}
