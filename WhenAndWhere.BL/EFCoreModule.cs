using Autofac;
using AutoMapper;
using Azure.Data.Tables;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.EFCore.Repository;
using WhenAndWhere.Infrastructure.EFCore.UnitOfWork;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;
using WhenAndWhere.Infrastructure.EFCore.Query;
using WhenAndWhere.Infrastructure.Query;

namespace WhenAndWhere.BL
{
    public class EFCoreModule : Module
    {
        private readonly NpgsqlConnection _connection;
        public EFCoreModule(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var dbContextOptions = new DbContextOptionsBuilder<WhenAndWhereDBContext>()
                .UseNpgsql(_connection)
                .UseLazyLoadingProxies()
                .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning))
                .Options;

            builder.Register<WhenAndWhereDBContext>(ctx => new WhenAndWhereDBContext(dbContextOptions))
                .InstancePerDependency()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));

            builder.RegisterType<EFUnitOfWork>()
                .InstancePerLifetimeScope()
                .As<IUnitOfWork>()
                //.As<IEFCoreUnitOfWork>() // This registres UnitOfWork for both interfaces. One "internal" and one "external"
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));

            builder.RegisterGeneric(typeof(EntityFrameworkQuery<>))
                .As(typeof(IQuery<>))
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));

            builder.RegisterGeneric(typeof(EFGenericRepository<>))
                .As(typeof(IRepository<>))
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));

            // AutoMapper Profile
            builder.RegisterType<EFCoreProfile>()
                .As<Profile>()
                .AutoActivate();

            builder.Register<TableClient>(ctx => new TableClient(
                new Uri("https://485607.table.core.windows.net/WhenAndWhere"),
                "WhenAndWhere",
                new TableSharedKeyCredential("485607", "wOX2fcfPw5x6lGNI1x1rV5XR+b8eEgLRkWXH6en+qNIU36xxRBOcK8fd7aX1PkVXJcux5O63KiBu+AStpHj7mA==")));

            builder.Register<QueueClient>(ctx => new QueueClient(
                "Endpoint=sb://whenandwhere.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=6igbXdA0kfjpbocv1l3htB5OwoT4eCQLX+ASbHqz3XI=",
                "meetup-table-queue"
            ));
        }
    }
}
