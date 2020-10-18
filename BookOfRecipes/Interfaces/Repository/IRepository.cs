using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes
{
    public interface IRepository
    {
        IEnumerable<T1> GetSheet<T1>();
        void SetSheet<T>(List<T> sheet);
        void SetSheet<T>(T entity);
        void DeleteSheet<T>(T entity);
    }
}