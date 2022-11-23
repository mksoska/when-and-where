using Autofac;
using Microsoft.Data.Sqlite;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.BL.Query;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.EFCore;

namespace WhenAndWhere;

public class Bootstrapper : IDisposable
{
    private SqliteConnection _sqliteConnection;
    public IContainer Container { get; init; }

    public Bootstrapper(Provider provider)
    {
        _sqliteConnection = new SqliteConnection("Data Source=WhenAndWhere.sqlite;Cache=Shared");
        _sqliteConnection.Open();

        var builder = new ContainerBuilder();

        // Choose ORM
        Module module = provider switch
        {
            Provider.EFCore => new EFCoreModule(_sqliteConnection),
        };
        builder.RegisterModule(module);

        // Register BL Services
        builder.RegisterGeneric(typeof(GenericService<,>))
            .As(typeof(IGenericService<,>))
            .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(QueryObjectGeneric<,>))
            .InstancePerLifetimeScope();

        builder.RegisterType<MeetupService>()
            .As<MeetupService>()
            .InstancePerLifetimeScope();

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
        EFCore
    }
}
