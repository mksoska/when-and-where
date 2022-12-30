using Microsoft.EntityFrameworkCore.Storage;
using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.Infrastructure.EFCore.UnitOfWork;

public class EFUnitOfWork : IUnitOfWork
{
    private WhenAndWhereDBContext _context;
    private IDbContextTransaction _transaction;

    public WhenAndWhereDBContext Context => _context;

    public EFUnitOfWork(WhenAndWhereDBContext context)
    {
        _context = context;
        _transaction = context.Database.BeginTransaction();
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _context.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        _transaction.DisposeAsync();
        _context.DisposeAsync();

        return ValueTask.CompletedTask;
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
        await _context.SaveChangesAsync();
        _transaction = _context.Database.BeginTransaction();
    }
}