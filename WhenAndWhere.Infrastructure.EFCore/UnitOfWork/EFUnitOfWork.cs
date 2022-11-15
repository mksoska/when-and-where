using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Repository;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.Infrastructure.EFCore.UnitOfWork;

public class EFUnitOfWork : IUnitOfWork
{
    public WhenAndWhereDBContext Context { get; } = new();

    public EFUnitOfWork(
        WhenAndWhereDBContext dbContext,
        IRepository<Address> addressRepository,
        IRepository<Meetup> meetupRepository,
        IRepository<UserRole> userRoleRepository,
        IRepository<Option> optionRepository,
        IRepository<Role> roleRepository,
        IRepository<User> userRepository,
        IRepository<UserMeetup> userMeetupRepository,
        IRepository<UserOption> userOptionRepository
    ) 
    {
        Context = dbContext;
        AddressRepository = addressRepository;
        MeetupRepository = meetupRepository;
        UserRoleRepository = userRoleRepository;
        OptionRepository = optionRepository;
        RoleRepository = roleRepository;
        UserRepository = userRepository;
        UserMeetupRepository = userMeetupRepository;
        UserOptionRepository = userOptionRepository;
    }

    public IRepository<TEntity> TEntityRepository<TEntity>(IRepository<TEntity> repository) where TEntity : class
    {
        if (repository == null)
        {
            repository = new EFGenericRepository<TEntity>(Context);
        }
        return repository;
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