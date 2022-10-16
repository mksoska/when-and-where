using WhenAndWhere.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenAndWhere.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Persists all changes made within this unit of work.
        /// </summary>
        Task Commit();
    }
}
