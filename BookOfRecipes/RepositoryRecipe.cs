using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes
{
    class RepositoryRecipe : IRepository<ModelRecipe>
    {
        //Объявляем ссылку типа ContextEntity
        public ContextEntity contextEntity;
        public RepositoryRecipe(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
        }
        public void Add(ModelRecipe entity)
        {
            if (entity != null)
                contextEntity.RecipeSheet.Add(entity);
        }

        public void AddRange(IEnumerable<ModelRecipe> entities)
        {
            contextEntity.RecipeSheet.AddRange(entities);
        }

        public void Delete(ModelRecipe entity)
        {
            if (entity != null)
                contextEntity.RecipeSheet.Remove(entity);
        }
        public IEnumerable<ModelRecipe> GetAll()
        {
            return contextEntity.RecipeSheet.OrderBy(x => x.Id).ToList();
        }
        public ModelRecipe GetById(int id)
        {
            return contextEntity.RecipeSheet.Find(x => x.Id == id);
        }
    }
}
