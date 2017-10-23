using LukeVo.DataFW.Data;
using LukeVo.DataFW.Data.Entities;
using LukeVo.DataFW.Data.Repositories;
using LukeVo.DataFW.EF.Attributes;
using LukeVo.DataFW.EF.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LukeVo.DataFW.EF.Repositories
{
    public class BaseEfRepository<TEntity> : IRepository<TEntity>
       where TEntity : class, IEntity
    {

        protected DbContext dbContext;
        protected DbSet<TEntity> dbSet;

        public bool HasNoDefaultValueAttribute { get; private set; }
        public bool IsEntityActivable { get; private set; }

        public BaseEfRepository(DbContext context)
        {
            this.dbContext = context;
            this.dbSet = this.dbContext.Set<TEntity>();

            this.HasNoDefaultValueAttribute = this.GetType().GetTypeInfo()
                .GetCustomAttributes(typeof(NoDefaultValueAttribute), true)
                .Any();
            this.IsEntityActivable = typeof(TEntity).IsAssignableFrom(typeof(IEntity));
        }

        public virtual IQueryable<TEntity> Get()
        {
            return this.dbSet.AsQueryable();
        }

        public virtual TEntity Get<TKey>(TKey id)
        {
            return this.dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dbSet.Where(predicate);
        }

        public virtual IQueryable<TEntity> GetActive()
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> getActive = q => ((IActivable)q).Active;
                getActive = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(getActive);
                return this.Get().Where(getActive);
            }
            else
            {
                return this.Get();
            }
        }

        public virtual IQueryable<TEntity> GetActive(Expression<Func<TEntity, bool>> predicate)
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> getActive = q => ((IActivable)q).Active;
                getActive = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(getActive);
                return this.Get().Where(getActive)
                    .Where(predicate);
            }
            else
            {
                return this.Get(predicate);
            }
        }

        public virtual TEntity FirstOrDefault()
        {
            return this.dbSet.FirstOrDefault();
        }

        public virtual TEntity FirstOrDefaultActive()
        {
            if (this.IsEntityActivable)
            {
                Expression<Func<TEntity, bool>> getActive = q => ((IActivable)q).Active;
                getActive = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(getActive);
                return this.dbSet.FirstOrDefault(getActive);
            }
            else
            {
                return this.FirstOrDefault();
            }
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dbSet.FirstOrDefault(predicate);
        }

        public virtual TEntity FirstOrDefaultActive(Expression<Func<TEntity, bool>> predicate)
        {
            if (this.IsEntityActivable)
            {
                Expression<Func<TEntity, bool>> getActive = q => ((IActivable)q).Active;
                getActive = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(getActive);
                return this.dbSet.Where(predicate).FirstOrDefault(getActive);
            }
            else
            {
                return this.FirstOrDefault(predicate);
            }
        }

        public virtual void Add(TEntity entity)
        {
            if (!this.HasNoDefaultValueAttribute)
            {
                entity.SetDefaultValues();
            }

            this.dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (!this.HasNoDefaultValueAttribute)
            {
                entity.SetDefaultUpdateValues();
            }

            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Activate(TEntity entity)
        {
            this.ThrowIfNotActivable();

            ((IActivable)entity).Active = true;
            this.Update(entity);
        }

        public virtual void Deactivate(TEntity entity)
        {
            this.ThrowIfNotActivable();

            ((IActivable)entity).Active = false;
            this.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public virtual void Refresh(TEntity entity)
        {
            this.dbContext.Entry(entity).Reload();
        }

    }

    public class BaseEfRepositoryAsync<TEntity> : BaseEfRepository<TEntity>, IRepositoryAsync<TEntity>
       where TEntity : class, IEntity
    {

        public BaseEfRepositoryAsync(DbContext context) : base(context) { }

        public async Task<TEntity> GetAsync<TKey>(TKey id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await this.dbSet.FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultActiveAsync()
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> getActive = q => ((IActivable)q).Active;
                getActive = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(getActive);
                return await this.dbSet.FirstOrDefaultAsync(getActive);
            }
            else
            {
                return await this.FirstOrDefaultAsync();
            }
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dbSet.FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> FirstOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> getActive = q => ((IActivable)q).Active;
                getActive = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(getActive);
                return this.dbSet.Where(predicate).FirstOrDefaultAsync(getActive);
            }
            else
            {
                return this.FirstOrDefaultAsync(predicate);
            }
        }
    }

}
