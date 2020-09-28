using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes
{
    class RepositoryIngredient : IRepository<ModelIngredient>
    {
        //Объявляем ссылку типа ContextEntity
        public ContextEntity contextEntity;
        public RepositoryIngredient(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
        }
        public void AddRange(IEnumerable<ModelIngredient> entities)
        {
            contextEntity.IngredientSheet.AddRange(entities);
        }
        public void Add(ModelIngredient entity)
        {
            if (entity != null)
                contextEntity.IngredientSheet.Add(entity);
        }
        public void Delete(ModelIngredient entity)
        {
            if (entity != null)
                contextEntity.IngredientSheet.Remove(entity);
        }
        public IEnumerable<ModelIngredient> GetAll()
        {
            return contextEntity.IngredientSheet.OrderBy(x => x.Id).ToList();
        }
        ModelIngredient IRepository<ModelIngredient>.GetById(int id)
        {
            return contextEntity.IngredientSheet.Find(x => x.Id == id);
        }
    }
}
