using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<Meetup> MeetupRepository { get; }
    IRepository<User> UserRepository { get; }
    IRepository<UserMeetup> UserMeetupRepository { get; }
    IRepository<UserOption> UserOptionRepository { get; }
    IRepository<UserRole> UserRoleRepository { get; }
    IRepository<Option> OptionRepository { get; }
    IRepository<Address> AddressRepository { get; }
    IRepository<Role> RoleRepository { get; }

    /// <summary>
    /// Persists all changes made within this unit of work.
    /// </summary>
    Task Commit();
}