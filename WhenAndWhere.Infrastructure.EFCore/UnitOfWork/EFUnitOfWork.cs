using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Repository;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenAndWhere.Infrastructure.EFCore.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public WhenAndWhereDBContext Context { get; } = new();
        private IRepository<Address> addressRepository;
        private IRepository<Meetup> meetupRepository;
        private IRepository<Option> optionRepository;
        private IRepository<Role> roleRepository;
        private IRepository<User> userRepository;

        public EFUnitOfWork(WhenAndWhereDBContext dbContext)
        {
            Context = dbContext;
        }

        public IRepository<TEntity> TEntityRepository<TEntity>(IRepository<TEntity> repository) where TEntity : class
        {
            if (repository == null)
            {
                repository = new EFGenericRepository<TEntity>(Context);
            }
            return repository;
        }

        public IRepository<Address> AddressRepository { get { return TEntityRepository(addressRepository); } }

        public IRepository<Meetup> MeetupRepository { get { return TEntityRepository(meetupRepository); } }

        public IRepository<Option> OptionRepository { get { return TEntityRepository(optionRepository); } }

        public IRepository<Role> RoleRepository { get { return TEntityRepository(roleRepository); } }

        public IRepository<User> UserRepository { get { return TEntityRepository(userRepository); } }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
