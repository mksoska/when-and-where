using Autofac;
using WhenAndWhere;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

using var _ioc = new Bootstrapper(Bootstrapper.Provider.EFCore);

using (var scope = _ioc.Container.BeginLifetimeScope())
{
    using var uow = scope.Resolve<IUnitOfWork>();
    var genericRepository = scope.Resolve<IRepository<IEntity>>();
    var genericService = scope.Resolve<IGenericService<AddressDTO, Address>>();
}    
