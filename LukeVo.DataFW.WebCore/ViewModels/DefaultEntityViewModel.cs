using AutoMapper;
using LukeVo.DataFW.Data.Entities;
using LukeVo.DataFW.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LukeVo.DataFW.WebCore.ViewModels
{

    public class DefaultEntityViewModel<TEntity> : IEntityViewModel<TEntity>
        where TEntity : class, IEntity
    {
        
        public virtual IEntityViewModel<TEntity> CopyFrom(TEntity source)
        {
            Mapper.Map(source, this);
            return this;
        }

        public virtual IEntityViewModel<TEntity> CopyTo(TEntity target)
        {
            Mapper.Map(this, target);
            return this;
        }

        public virtual TEntity ToEntity()
        {
            return Mapper.Map<TEntity>(this);
        }

    }

}
