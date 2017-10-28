using LukeVo.DataFW.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LukeVo.DataFW.Data.ViewModels
{

    public interface IEntityViewModel<TEntity> where TEntity: IEntity
    {

        IEntityViewModel<TEntity> CopyTo(TEntity target);
        IEntityViewModel<TEntity> CopyFrom(TEntity source);

    }
    
}
