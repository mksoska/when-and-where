using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.Infrastructure.EFCore.UnitOfWork;

public class EFUnitOfWork : IUnitOfWork
{
    private WhenAndWhereDBContext _context;
    private TransactionScope _scope;

    public WhenAndWhereDBContext Context => _context;

    public EFUnitOfWork(WhenAndWhereDBContext context)
    {
        _context = context;
        _scope = new TransactionScope();
    }

    public void Dispose()
    {
        _scope.Dispose();
        _context.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        _scope.Dispose();
        _context.DisposeAsync();

        return ValueTask.CompletedTask;
    }

    public async Task RollbackAsync()
    {
        _scope = new TransactionScope();
    }

    public async Task CommitAsync()
    {
        _scope.Complete();
        await _context.SaveChangesAsync();
        _scope = new TransactionScope();
    }
}