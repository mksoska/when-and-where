using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Repository;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.Infrastructure.EFCore.UnitOfWork;

public class EFUnitOfWork : IUnitOfWork
{
    public WhenAndWhereDBContext Context { get; }

    public EFUnitOfWork(
        WhenAndWhereDBContext dbContext
    ) 
    {
        Context = dbContext;
        AddressRepository = new EFGenericRepository<Address>(dbContext);
        MeetupRepository = new EFGenericRepository<Meetup>(dbContext);
        UserRoleRepository = new EFGenericRepository<UserRole>(dbContext);
        OptionRepository = new EFGenericRepository<Option>(dbContext);
        RoleRepository = new EFGenericRepository<Role>(dbContext);
        UserRepository = new EFGenericRepository<User>(dbContext);
        UserMeetupRepository = new EFGenericRepository<UserMeetup>(dbContext);
        UserOptionRepository = new EFGenericRepository<UserOption>(dbContext);
    }

    public IRepository<Address> AddressRepository { get; }
    public IRepository<Meetup> MeetupRepository { get; }
    public IRepository<UserRole> UserRoleRepository { get; }
    public IRepository<Option> OptionRepository { get; }
    public IRepository<Role> RoleRepository { get; }
    public IRepository<User> UserRepository { get; }
    public IRepository<UserMeetup> UserMeetupRepository { get; }
    public IRepository<UserOption> UserOptionRepository { get; }

    public async Task Commit()
    {
        await Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}