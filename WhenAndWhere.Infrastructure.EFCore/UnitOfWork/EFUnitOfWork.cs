using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.Infrastructure.EFCore.UnitOfWork;

public class EFUnitOfWork : IUnitOfWork
{
    private WhenAndWhereDBContext _context;

    public WhenAndWhereDBContext Context => _context;

    public EFUnitOfWork(WhenAndWhereDBContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        _context.DisposeAsync();

        return ValueTask.CompletedTask;
    }

    public async Task RollbackAsync() { }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}