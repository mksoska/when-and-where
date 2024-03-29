﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.UnitOfWork;

namespace WhenAndWhere.Infrastructure.EFCore.Test;

public class TestContextInitializer
{
    protected WhenAndWhereDBContext dbContext;
    protected EFUnitOfWork unitOfWork;

    public TestContextInitializer()
    {
        var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

        var dbContextOptions = new DbContextOptionsBuilder<WhenAndWhereDBContext>()
            .UseInMemoryDatabase(databaseName: myDatabaseName)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .UseLazyLoadingProxies()
            .Options;

        dbContext = new WhenAndWhereDBContext(dbContextOptions);

        var userJohan = new User { Id = 1, FirstName = "Johan", Surname = "Kovalchuck", Email = "aa@bb.cc", PhoneNumber = "1111111111", Avatar = new byte[] { 0xAB, 0xCD, 0xEF } };
        var userMatys = new User { Id = 2, FirstName = "Matysku", Surname = "Popisku", Email = "dd@ee.ff", PhoneNumber = "2222222222", Avatar = new byte[] { 0xFE, 0xDC, 0xBA } };
        var userDavid = new User { Id = 3, FirstName = "Davit", Surname = "Maslovich", Email = "gg@hh.ii", PhoneNumber = "3333333333", Avatar = new byte[] { 0xFE, 0xDC, 0xEA } };
        
        var option1 = new Option { Id = 1, Label = "Spravna volba", Start = new DateTime(2022, 10, 12), City = "Brno", Number = "9", State = "CZE", Street = "Ryšánková", ZipCode = "61300" };

        var meetupSlopanie = new Meetup { Id = 1, Name = "Slopanie", Description = "Pravidelna najebavka", OptionsFrom = new DateTime(2022, 10, 9), OptionsTo = new DateTime(2022, 10, 18), Owner = userJohan, Type = MeetupType.Drinking, Options = new List<Option> { option1 }, Logo = new byte[] { 0xFE, 0xDC, 0xEA } };

        var johanSlopanie = new UserMeetup { UserId = userJohan.Id, MeetupId = meetupSlopanie.Id, State = StateEnum.Accepted, DateInvited = DateTime.Now };
        var matysSlopanie = new UserMeetup { UserId = userMatys.Id, MeetupId = meetupSlopanie.Id, State = StateEnum.Accepted, DateInvited = DateTime.Now };
        var davidSlopanie = new UserMeetup { UserId = userDavid.Id, MeetupId = meetupSlopanie.Id, State = StateEnum.Accepted, DateInvited = DateTime.Now };

        meetupSlopanie.InvitedUsers = new List<UserMeetup> { johanSlopanie, matysSlopanie, davidSlopanie };


        dbContext.User.Add(userJohan);
        dbContext.User.Add(userMatys);
        dbContext.User.Add(userDavid);
        
        dbContext.Meetup.Add(meetupSlopanie);

        dbContext.UserMeetup.Add(johanSlopanie);
        dbContext.UserMeetup.Add(matysSlopanie);
        dbContext.UserMeetup.Add(davidSlopanie);

        dbContext.SaveChanges();

        unitOfWork = new EFUnitOfWork(dbContext);
    }
}