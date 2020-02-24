using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        TEntity Get(object id);
        TEntity Get(Expression<Func<TEntity, bool>> match);
        TEntity Find(Expression<Func<TEntity, bool>> match);
        IQueryable<TEntity> All();
        IQueryable<TEntity> AllUntracked();
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);
        ICollection<TEntity> GetAll(bool isNoTracked = true);
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match, bool isNoTracked = true);
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entityList);
        void Insert(params TEntity[] entities);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity updated);
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] updateProperties);
        void Update(IEnumerable<TEntity> entities);
        void Remove(object id);
        void Remove(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        void Delete(IEnumerable<TEntity> entities);
        bool Contains(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
    }
}
