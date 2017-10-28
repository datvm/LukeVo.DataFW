using LukeVo.DataFW.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LukeVo.DataFW.WebCore
{

    public class DefaultUnitOfWork : IUnitOfWork
    {

        protected DbContext DbContext { get; set; }

        public DefaultUnitOfWork(DbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public int Commit()
        {
            return this.DbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.DbContext.Dispose();
        }
    }

    public class DefaultUnitOfWorkAsync : DefaultUnitOfWork, IUnitOfWorkAsync
    {
        public DefaultUnitOfWorkAsync(DbContext dbContext) : base(dbContext) { }

        public async Task<int> CommitAsync()
        {
            return await this.DbContext.SaveChangesAsync();
        }
    }

}
