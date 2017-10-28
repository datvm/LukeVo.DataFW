using LukeVo.DataFW.Data;
using LukeVo.DataFW.Data.Entities;
using LukeVo.DataFW.Data.Repositories;
using LukeVo.DataFW.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LukeVo.DataFW.EF.Services
{
    public class DefaultEfService<TEntity> : IService<TEntity> where TEntity : class, IEntity
    {
        protected IUnitOfWork unitOfWork;
        protected IRepository<TEntity> repository;

        public DefaultEfService(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        public TEntity Get<TKey>(TKey id)
        {
            return this.repository.Get(id);
        }

        public TEntity GetActive<TKey>(TKey id)
        {
            var result = this.repository.Get(id);
            return ((IActivable)result).Active ? result : null;
        }

        public virtual void Create(TEntity entity)
        {
            this.OnCreate(entity);
            this.Save();
        }

        public virtual void Update(TEntity entity)
        {
            this.OnUpdate(entity);
            this.Save();
        }

        public virtual void Delete(TEntity entity)
        {
            this.OnDelete(entity);
            this.Save();
        }

        public virtual void Activate(TEntity entity)
        {
            this.OnActivate(entity);
            this.Save();
        }

        public virtual void Deactivate(TEntity entity)
        {
            this.OnDeactivate(entity);
            this.Save();
        }

        public virtual void Save()
        {
            this.unitOfWork.Commit();
        }

        public virtual void Refresh(TEntity entity)
        {
            this.repository.Refresh(entity);
        }
        
        #region Common Calls

        protected virtual void OnCreate(TEntity entity)
        {
            this.repository.Add(entity);
        }

        protected virtual void OnUpdate(TEntity entity)
        {
            this.repository.Update(entity);
        }

        protected virtual void OnActivate(TEntity entity)
        {
            this.repository.Activate(entity);
        }

        protected virtual void OnDeactivate(TEntity entity)
        {
            this.repository.Deactivate(entity);
        }

        protected virtual void OnDelete(TEntity entity)
        {
            this.repository.Delete(entity);
        }

        #endregion

    }

    public class DefaultEfServiceAsync<TEntity> : DefaultEfService<TEntity>, IServiceAsync<TEntity> where TEntity : class, IEntity
    {

        protected new IUnitOfWorkAsync unitOfWork;
        protected new IRepositoryAsync<TEntity> repository;

        public DefaultEfServiceAsync(IUnitOfWorkAsync unitOfWork, IRepositoryAsync<TEntity> repository) : base(unitOfWork, repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        public virtual Task<TEntity> GetAsync<TKey>(TKey id)
        {
            return this.repository.GetAsync(id);
        }

        public virtual async Task<TEntity> GetActiveAsync<TKey>(TKey id)
        {
            var result = await this.repository.GetAsync(id);

            if (result != null && (result as IActivable)?.Active == true)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public virtual Task SaveAsync()
        {
            return this.unitOfWork.CommitAsync();
        }

        public virtual Task CreateAsync(TEntity entity)
        {
            this.OnCreate(entity);
            return this.SaveAsync();
        }

        public virtual Task ActivateAsync(TEntity entity)
        {
            this.OnActivate(entity);
            return this.SaveAsync();
        }

        public virtual Task DeactivateAsync(TEntity entity)
        {
            this.OnDeactivate(entity);
            return this.SaveAsync();
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            this.OnUpdate(entity);
            return this.SaveAsync();
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            this.OnDelete(entity);
            return this.SaveAsync();
        }

    }

}
