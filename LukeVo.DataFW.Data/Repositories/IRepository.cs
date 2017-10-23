using LukeVo.DataFW.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LukeVo.DataFW.Data.Repositories
{
    public interface IRepository { }

    public interface IRepository<TEntity> : IRepository
        where TEntity : IEntity
    {
        TEntity Get<TKey>(TKey id);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetActive();
        IQueryable<TEntity> GetActive(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault();
        TEntity FirstOrDefaultActive();
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefaultActive(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Activate(TEntity entity);
        void Deactivate(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        void Refresh(TEntity entity);
        
    }

    public interface IRepositoryAsync<TEntity> : IRepository<TEntity>
         where TEntity : IEntity
    {
        Task<TEntity> GetAsync<TKey>(TKey id);
        Task<TEntity> FirstOrDefaultAsync();
        Task<TEntity> FirstOrDefaultActiveAsync();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate);
    }

}
