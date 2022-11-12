using Autofac;
using Microsoft.Data.Sqlite;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.EFCore;

namespace WhenAndWhere
{
    public class Bootstrapper : IDisposable
    {
        private SqliteConnection _sqliteConnection;
        public IContainer Container { get; init; }

        public Bootstrapper(Provider provider)
        {
            // This configures in-memory database
            _sqliteConnection = new SqliteConnection("Data Source=WhenAndWhere.sqlite;Cache=Shared");
            _sqliteConnection.Open();

            var builder = new ContainerBuilder();

            // Coose ORM
            Module module = provider switch
            {
                Provider.EFCore => new EFCoreModule(_sqliteConnection),
            };
            builder.RegisterModule(module);

            // Register BL Services
            builder.RegisterType<AddressService>()
                .As<IAddressService>();


            // Email
            //builder.RegisterType<EmailService>()
            //    .As<IEmailService>();
            //var smtpClient = new SmtpClient("smtp-relay.sendinblue.com", 587)
            //{
            //    Credentials = new System.Net.NetworkCredential("", "")
            //};
            //builder.Register(ctx => new SmtpSender(smtpClient))
            //    .As<ISender>();

            // See BootstraperExtensions
            builder.RegisterAutoMapper();

            Container = builder.Build();

            // Init database
            InitDatabaseEFCore();
        }

        private void InitDatabaseEFCore()
        {
            //using var context = Container.Resolve<DAL.EFCore.CourseContext>();
            using var context = Container.Resolve<WhenAndWhereDBContext>();
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _sqliteConnection.Dispose();
            Container.Dispose();
        }

        public enum Provider
        {
            EFCore,
            Dapper
        }
    }
}
