using Autofac;
using WhenAndWhere;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

using var _ioc = new Bootstrapper(Bootstrapper.Provider.EFCore);

using (var scope = _ioc.Container.BeginLifetimeScope())
{
    using var uow = scope.Resolve<IUnitOfWork>();
    var genericRepository = scope.Resolve<IRepository<IEntity>>();


}    
