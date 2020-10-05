using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes
{
    class RepositoryCategory : IRepository<Category>
    {
        //Объявляем ссылку типа ContextEntity
        ContextEntity contextEntity;
        public RepositoryCategory(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
        }
        public void Add(Category entity)
        {
            if (entity != null)
                contextEntity.CategorySheet.Add(entity);
        }
        public void AddRange(IEnumerable<Category> entities)
        {
            contextEntity.CategorySheet.AddRange(entities);
        }
        public void Delete(Category entity)
        {
            if (entity != null)
                contextEntity.CategorySheet.Remove(entity);
        }

        public IEnumerable<Category> GetAll()
        {
            return contextEntity.CategorySheet.OrderBy(x => x.Id).ToList();
        }

        public Category GetById(int id)
        {
            return contextEntity.CategorySheet.Find(x => x.Id == id);
        }
    }
}