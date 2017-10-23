using LukeVo.DataFW.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LukeVo.DataFW.EF
{

    public class EfUnitOfWork : IUnitOfWorkAsync
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        private DbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the EfUnitOfWork class.
        /// </summary>
        /// <param name="context">The context object</param>
        public EfUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.
        /// This can include state entries for entities and/or relationships. Relationship state entries are 
        /// created for many-to-many relationships and relationships where there is no foreign 
        /// key property included in the entity class (often referred to as independent associations).</returns>
        public int Commit()
        {
            return this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.
        /// This can include state entries for entities and/or relationships. Relationship state entries are 
        /// created for many-to-many relationships and relationships where there is no foreign 
        /// key property included in the entity class (often referred to as independent associations).</returns>
        public Task<int> CommitAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.dbContext != null)
                {
                    this.dbContext.Dispose();
                    this.dbContext = null;
                }
            }
        }
    }
}
