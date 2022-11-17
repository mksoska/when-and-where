using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Persists all changes made within this unit of work.
    /// </summary>
    Task CommitAsync();
    
    /// <summary>
    /// Rollbacks all changes made within this unit of work.
    /// </summary>
    Task RollbackAsync();
}