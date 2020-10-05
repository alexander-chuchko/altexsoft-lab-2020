using System;
using System.Collections.Generic;
using System.Text;

namespace BookOfRecipes
{
    interface IUnitOfWork
    {
        void Commit();
        UnitOfWork GetLink();
    }
}
