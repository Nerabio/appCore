using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDictionary<Guid, IDbContextTransaction> _transaction;

        public UnitOfWork()
        {
            _dbContext = new ApplicationDbContext();
            _transaction = new Dictionary<Guid, IDbContextTransaction>();
        }

        public bool IsAnyChanges => _dbContext.ChangeTracker.Entries()
            .Any(e => e.State == EntityState.Added || e.State == EntityState.Deleted ||
                      e.State == EntityState.Modified);

        public int SaveChanges()
        {
            var result = 0;
            try
            {
                result = _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //LogService.Log.Error("DbContext SaveChanges Error. ", ex);
                throw;
            }

            return result;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new()
        {
            return new GenericRepository<TEntity>(_dbContext);
        }

        public Guid BeginTransaction()
        {
            var key = Guid.NewGuid();
            var transaction = _dbContext.Database.BeginTransaction();
            _transaction.Add(key, transaction);

            return key;
        }

        public void CommitTransaction(Guid key)
        {
            IDbContextTransaction transaction;
            if (!_transaction.TryGetValue(key, out transaction))
                return;

            _transaction.Remove(key);
            transaction.Commit();
            transaction.Dispose();
        }

        public void RollbackTransaction(Guid key)
        {
            IDbContextTransaction transaction;
            if (!_transaction.TryGetValue(key, out transaction))
                return;

            _transaction.Remove(key);
            transaction.Rollback();
            transaction.Dispose();
        }

        public void ExecuteSql<T>(string sql, params object[] args)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TOut> ExecuteSql<TContext, TOut>(string sql, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
