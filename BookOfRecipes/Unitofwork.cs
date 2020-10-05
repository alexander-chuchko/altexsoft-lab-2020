
using System;
using System.Collections.Generic;
using System.Text;


namespace BookOfRecipes
{
    class UnitOfWork : IDisposable, IUnitOfWork
    {
        ContextEntity contextEntity; 
        RepositoryCategory repositoryCategory;
        RepositoryIngredient repositoryIngredient; 
        RepositoryRecipe repositoryRecipe; 

        public UnitOfWork(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
        }
        public RepositoryCategory GetCategory
        {
            get
            {
                if (repositoryCategory == null)
                    repositoryCategory = new RepositoryCategory(contextEntity);
                return repositoryCategory;
            }
        }
        public RepositoryIngredient GetIngredient
        {
            get
            {
                if (repositoryIngredient == null)
                    repositoryIngredient = new RepositoryIngredient(contextEntity);
                return repositoryIngredient;
            }
        }
        public RepositoryRecipe GetRecipe
        {
            get
            {
                if (repositoryRecipe == null)
                    repositoryRecipe = new RepositoryRecipe(contextEntity);
                return repositoryRecipe;
            }
        }
        public void Commit()
        {
            contextEntity.SaveChanges();
        }

        UnitOfWork IUnitOfWork.GetLink()
        {
            UnitOfWork unitOfWork = new UnitOfWork(contextEntity);
            return unitOfWork;
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

