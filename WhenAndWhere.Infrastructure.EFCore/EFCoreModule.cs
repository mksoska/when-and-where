using Autofac;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WhenAndWhere.DTO;
using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.EFCore.Repository;
using WhenAndWhere.Infrastructure.EFCore.UnitOfWork;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.Infrastructure.EFCore
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
                .Options;

            builder.Register<WhenAndWhereDBContext>(ctx => new WhenAndWhereDBContext(dbContextOptions))
                .InstancePerDependency()
                .OnActivated(e => Console.WriteLine($"Build {e.Instance.GetType().Name}"));
            builder.RegisterType<EFUnitOfWork>()
                .InstancePerLifetimeScope() // This ensures only one UoW per Repos, as SQLite does not support nested transactions
                .As<IUnitOfWork>()
                //.As<IEFCoreUnitOfWork>() // This registres UnitOfWork for both interfaces. One "internal" and one "external"
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

    // Configuration for AutoMapper
    internal class EFCoreProfile : Profile
    {
        public EFCoreProfile()
        {
            CreateMap<AddressDTO, Address>().ReverseMap();
            //createmap<domain.course, entities.course>().reversemap();

            //createmap<domain.enrollment, entities.enrollment>()
            //    .formember(d => d.canceledtimestamp, m => m.mapfrom(s => s.canceledtimestamp.tonullable()))
            //    .formember(d => d.courseid, m => m.mapfrom(s => s.course.id))
            //    .formember(d => d.course, m => m.ignore());

            //createmap<entities.enrollment, domain.enrollment>()
            //    .formember(d => d.canceledtimestamp, m => m.mapfrom(s => s.canceledtimestamp.tooption()));
        }
    }
}
