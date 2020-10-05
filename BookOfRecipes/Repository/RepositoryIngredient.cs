using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes
{
    class RepositoryIngredient : IRepository<Ingredient>
    {
        //Объявляем ссылку типа ContextEntity
        ContextEntity contextEntity;
        public RepositoryIngredient(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
        }
        public void AddRange(IEnumerable<Ingredient> entities)
        {
            contextEntity.IngredientSheet.AddRange(entities);
        }
        public void Add(Ingredient entity)
        {
            if (entity != null)
                contextEntity.IngredientSheet.Add(entity);
        }
        public void Delete(Ingredient entity)
        {
            if (entity != null)
                contextEntity.IngredientSheet.Remove(entity);
        }
        public IEnumerable<Ingredient> GetAll()
        {
            return contextEntity.IngredientSheet.OrderBy(x => x.Id).ToList();
        }
        Ingredient IRepository<Ingredient>.GetById(int id)
        {
            return contextEntity.IngredientSheet.Find(x => x.Id == id);
        }
    }
}