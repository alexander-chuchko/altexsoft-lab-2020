
using BookOfRecipes.Repository;
using System;
using System.Collections.Generic;
using System.Text;


namespace BookOfRecipes
{
    class UnitOfWork : IDisposable, IUnitOfWork
    {
        ContextEntity contextEntity;
        RepositoryIngredient repositoryIngredient;
        RepositoryRecipe repositoryRecipe;
        RepositoryCategory repositoryCategory;
        RepositorySubcategory repositorySubcategory;

        public UnitOfWork(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
        }
        public RepositoryCategory Categories
        {
            get
            {
                if (repositoryCategory == null)
                    repositoryCategory = new RepositoryCategory(contextEntity);
                return repositoryCategory;
            }
        }
        public RepositoryIngredient Ingredients
        {
            get
            {
                if (repositoryIngredient == null)
                    repositoryIngredient = new RepositoryIngredient(contextEntity);
                return repositoryIngredient;
            }
        }
        public RepositoryRecipe Recipes
        {
            get
            {
                if (repositoryRecipe == null)
                    repositoryRecipe = new RepositoryRecipe(contextEntity);
                return repositoryRecipe;
            }
        }
        public RepositorySubcategory Subcategories
        {
            get
            {
                if (repositorySubcategory == null)
                    repositorySubcategory = new RepositorySubcategory(contextEntity);
                return repositorySubcategory;
            }
        }
        public void Commit()
        {
            contextEntity.SaveChanges();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    contextEntity.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

