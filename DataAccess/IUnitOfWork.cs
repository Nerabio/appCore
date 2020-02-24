using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IUnitOfWork
    {
        bool IsAnyChanges { get; }

        int SaveChanges();

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new();

        Guid BeginTransaction();

        void CommitTransaction(Guid key);

        void RollbackTransaction(Guid key);


    }
}
