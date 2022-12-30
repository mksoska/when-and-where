using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore;

namespace WhenAndWhere.BL.Tests;

public class TestContextInitializer
{
    protected WhenAndWhereDBContext dbContext;
    protected IMapper mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile(new EFCoreProfile()); }));

    public TestContextInitializer()
    {
        var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

        var dbContextOptions = new DbContextOptionsBuilder<WhenAndWhereDBContext>()
            .UseInMemoryDatabase(databaseName: myDatabaseName)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .UseLazyLoadingProxies()
            .Options;

        dbContext = new WhenAndWhereDBContext(dbContextOptions);

        var user1 = new User
        {
            Id = 1,
            Name = "Jozef",
            Surname = "Bohdan",
            Email = "and@hob.it",
            Avatar = new byte[] {0xAA, 0xAA, 0xAA},
            PhoneNumber = "0011223344"
        };

        var meetup1 = new Meetup
        {
            Id = 1,
            Name = "Komtr pivo",
            Logo = new byte[] { 0xAA, 0xAA, 0xAA },
            OwnerId = 1,
            Type = MeetupType.Drinking,
            Description = "blah blah blah",
            OptionsFrom = new DateTime(2022, 9, 14),
            OptionsTo = DateTime.Now
        };

        var user2 = new User
        {
            Id = 2,
            Name = "Jozef",
            Surname = "Patrovic",
            Email = "cerstvy@vzduch.chyba",
            Avatar = new byte[] {0xAA, 0xAA, 0xAA},
            PhoneNumber = "0011223355"
        };
        var user3 = new User
        {
            Id = 3,
            Name = "Eugen",
            Surname = "Patrovic",
            Email = "aj@repujem.trosku",
            Avatar = new byte[] {0xAA, 0xAA, 0xAA},
            PhoneNumber = "0011223366"
        };

        dbContext.User.Add(user1);
        dbContext.User.Add(user2);
        dbContext.User.Add(user3);
        dbContext.Meetup.Add(meetup1);

        dbContext.SaveChanges();
    }
}