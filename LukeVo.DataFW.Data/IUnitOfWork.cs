using System;
using System.Threading.Tasks;

namespace LukeVo.DataFW.Data
{

    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }

    public interface IUnitOfWorkAsync: IUnitOfWork
    {
        Task<int> CommitAsync();
    }

}
