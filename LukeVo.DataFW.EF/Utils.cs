using LukeVo.DataFW.Data.Entities;
using LukeVo.DataFW.Data.Repositories;
using LukeVo.DataFW.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LukeVo.DataFW.EF
{
    public static class Utils
    {

        internal static NotSupportedException CreateNotActivableException<TEntity>()
        {
            return new NotSupportedException("TEntity must implement IActivable to use this method. TEntity: " + typeof(TEntity).FullName);
        }

        internal static void ThrowIfNotActivable<TEntity>(this DefaultEfRepository<TEntity> repository)
            where TEntity : class, IEntity
        {
            if (!repository.IsEntityActivable)
            {
                throw Utils.CreateNotActivableException<TEntity>();
            }
        }

    }
}
