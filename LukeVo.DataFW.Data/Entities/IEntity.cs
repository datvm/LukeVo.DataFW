using System;
using System.Collections.Generic;
using System.Text;

namespace LukeVo.DataFW.Data.Entities
{
    public interface IEntity { }

    public interface IActivable
    {
        bool Active { get; set; }
    }

    public interface ITrackCreatedTime
    {
        DateTime CreatedTime { get; set; }
    }

    public interface IAuditable : ITrackCreatedTime
    {
        DateTime UpdatedTime { get; set; }
    }

    public interface IPosition
    {
        int Position { get; set; }
    }

    public interface IActivablePosition : IActivable, IPosition { }
}
