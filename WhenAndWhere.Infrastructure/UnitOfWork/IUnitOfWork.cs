namespace WhenAndWhere.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Persists all changes made within this unit of work.
    /// </summary>
    Task Commit();
}