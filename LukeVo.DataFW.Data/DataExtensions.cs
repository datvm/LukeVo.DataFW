using LukeVo.DataFW.Data.Entities;
using LukeVo.DataFW.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LukeVo.DataFW.Data
{
    public static class Extensions
    {

        public static void SetDefaultValues(this IEntity entity)
        {
            if (entity is IActivable)
            {
                ((IActivable)entity).Active = true;
            }

            if (entity is ITrackCreatedTime)
            {
                ((ITrackCreatedTime)entity).CreatedTime = DateTime.UtcNow;
            }

            if (entity is IAuditable)
            {
                ((IAuditable)entity).UpdatedTime = DateTime.Now;
            }
        }

        public static void SetDefaultUpdateValues(this IEntity entity)
        {
            if (entity is IAuditable)
            {
                ((IAuditable)entity).UpdatedTime = DateTime.Now;
            }
        }

    }

}
