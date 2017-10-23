using LukeVo.DataFW.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LukeVo.DataFW.Data.ViewModels
{

    public interface IEntityViewModel<T> where T: IEntity
    {
        
        void CopyTo(T target);
        void CopyFrom(T source);

    }
    
}
