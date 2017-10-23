using LukeVo.DataFW.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LukeVo.DataFW.Data.Services
{

    public interface IService { }

    public interface IService<TEntity> : IService where TEntity : class, IEntity
    {
        TEntity Get<TKey>(TKey id);

        void Create(TEntity entity);
        void Activate(TEntity entity);
        void Deactivate(TEntity entity);
        void Delete(TEntity entity);
        void Save();
        
    }

    public interface IServiceAsync<TEntity> : IService<TEntity> where TEntity : class, IEntity
    {

        Task<TEntity> GetAsync<TKey>(TKey id);
        Task<TEntity> GetActiveAsync<TKey>(TKey id);

        Task CreateAsync(TEntity entity);
        Task ActivateAsync(TEntity entity);
        Task DeactivateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task SaveAsync();

    }

}
