using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes
{
    class RepositoryCategory : IRepository<ModelCategory>
    {
        //Объявляем ссылку типа ContextEntity
        public ContextEntity contextEntity;
        public RepositoryCategory(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
        }
        public void Add(ModelCategory entity)
        {
            if (entity != null)
                contextEntity.CategorySheet.Add(entity);
        }
        public void AddRange(IEnumerable<ModelCategory> entities)
        {
            contextEntity.CategorySheet.AddRange(entities);
        }
        public void Delete(ModelCategory entity)
        {
            if (entity != null)
                contextEntity.CategorySheet.Remove(entity);
        }

        public IEnumerable<ModelCategory> GetAll()
        {
            return contextEntity.CategorySheet.OrderBy(x => x.Id).ToList();
        }

        public ModelCategory GetById(int id)
        {
            return contextEntity.CategorySheet.Find(x => x.Id == id);
        }
    }
}
