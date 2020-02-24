using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #region Get one model

        public TEntity Get(object id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> match)
        {
            return _dbSet.FirstOrDefault(match);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return _dbSet.SingleOrDefault(match);
        }

        #endregion

        #region Get collection

        public IQueryable<TEntity> All()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> AllUntracked()
        {
            return _dbSet.AsNoTracking();
        }



        public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

        public ICollection<TEntity> GetAll(bool isNoTracked = true)
        {
            return isNoTracked ? AllUntracked().ToList() : All().ToList();
        }

        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match, bool isNoTracked = true)
        {
            var collection = isNoTracked ? AllUntracked() : All();
            return collection.Where(match).ToList();
        }

        #endregion

        #region Create

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<TEntity> entityList)
        {
            _dbSet.AddRange(entityList);
        }

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public virtual void Insert(params TEntity[] entities) => _dbSet.AddRange(entities);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public virtual void Insert(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);
        #endregion

        #region Update

        public void Update(TEntity updated)
        {
            if (updated == null)
                return;

            _dbSet.Attach(updated);
            _dbContext.Entry(updated).State = EntityState.Modified;
        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] updateProperties)
        {
            _dbSet.Attach(entity);
            foreach (var property in updateProperties)
                _dbContext.Entry<TEntity>(entity).Property(property).IsModified = true;
        }

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void Update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);
        #endregion

        #region Remove

        public void Remove(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbSet.Where(predicate).ToList();
            if (entities.Count > 0)
                _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void Delete(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);
        #endregion


        #region Contains and Count

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }



        #endregion
    }
}
