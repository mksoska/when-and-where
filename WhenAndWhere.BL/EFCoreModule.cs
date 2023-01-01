using Autofac;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.EFCore.Repository;
using WhenAndWhere.Infrastructure.EFCore.UnitOfWork;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Query;
using WhenAndWhere.Infrastructure.Query;

namespace WhenAndWhere.BL
{
    public class EFCoreModule : Module
    {
        private SqliteConnection _connection;
        public EFCoreModule(SqliteConnection connection)
        {
            _connection = connection;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var dbContextOptions = new DbContextOptionsBuilder<WhenAndWhereDBContext>()
                .UseSqlite(_connection)
                .UseLazyLoadingProxies()
                .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning))
                .Options;

            builder.Register<WhenAndWhereDBContext>(ctx => new WhenAndWhereDBContext(dbContextOptions))
                .InstancePerDependency()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));

            builder.RegisterType<EFUnitOfWork>()
                .InstancePerDependency()
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
        }
    }
}
