using Autofac;
using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.Infrastructure;

public class AutofacInfrastructureConfig : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EFUnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerDependency();

        builder.RegisterType<WhenAndWhereDBContext>()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}