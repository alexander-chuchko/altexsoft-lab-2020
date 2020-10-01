using System;
using System.Collections.Generic;
using System.Text;


namespace BookOfRecipes
{
    class UnitOfWork : IDisposable
    {
        public ContextEntity contextEntity { get; set; }
        public RepositoryCategory repositoryCategory { get; set; }
        public RepositoryIngredient repositoryIngredient { get; set; }
        public RepositoryRecipe repositoryRecipe { get; set; }

        public UnitOfWork(ContextEntity contextEntity)
        {
            this.contextEntity = contextEntity;
            repositoryRecipe = new RepositoryRecipe(contextEntity);
            repositoryIngredient = new RepositoryIngredient(contextEntity);
            repositoryCategory = new RepositoryCategory(contextEntity);
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

