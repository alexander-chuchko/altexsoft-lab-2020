using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookOfRecipes
{
    class RepositoryRecipe : IRepository<Recipe>
    {
        //Объявляем ссылку типа ContextEntity
        ContextEntity contextEntity;
        public RepositoryRecipe(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
        }
        public void Add(Recipe entity)
        {
            if (entity != null)
                contextEntity.RecipeSheet.Add(entity);
        }

        public void AddRange(IEnumerable<Recipe> entities)
        {
            contextEntity.RecipeSheet.AddRange(entities);
        }

        public void Delete(Recipe entity)
        {
            if (entity != null)
                contextEntity.RecipeSheet.Remove(entity);
        }
        public IEnumerable<Recipe> GetAll()
        {
            return contextEntity.RecipeSheet.OrderBy(x => x.Id).ToList();
        }
        public Recipe GetById(int id)
        {
            return contextEntity.RecipeSheet.Find(x => x.Id == id);
        }
    }
}