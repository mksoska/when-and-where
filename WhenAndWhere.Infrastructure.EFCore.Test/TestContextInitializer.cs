using System;
using Microsoft.EntityFrameworkCore;
using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.Infrastructure.EFCore.Test
{
    public class TestContextInitializer
    {
        protected readonly WhenAndWhereDBContext dbContext;

        public TestContextInitializer()
        {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

            var dbContextOptions = new DbContextOptionsBuilder<WhenAndWhereDBContext>()
                            .UseInMemoryDatabase(databaseName: myDatabaseName)
                            .UseLazyLoadingProxies()
                            .Options;

            dbContext = new WhenAndWhereDBContext(dbContextOptions);

            var userJohan = new User { Id = 1, Name = "Johan", Surname = "Kovalchuck", Email = "aa@bb.cc", PhoneNumber = "1111111111", Avatar = new byte[] { 0xAB, 0xCD, 0xEF } };
            var userMatys = new User { Id = 2, Name = "Matysku", Surname = "Popisku", Email = "dd@ee.ff", PhoneNumber = "2222222222", Avatar = new byte[] { 0xFE, 0xDC, 0xBA } };
            var userDavid = new User { Id = 3, Name = "Davit", Surname = "Maslovich", Email = "gg@hh.ii", PhoneNumber = "3333333333", Avatar = new byte[] { 0xFE, 0xDC, 0xEA } };

            var addressBrno = new Address { Id = 1, City = "Brno", Number = "9", State = "CZE", Street = "Ryšánková", ZipCode = "61300" };
            var addressBratislava = new Address { Id = 2, City = "Bratislava", Number = "7", State = "SK", Street = "Obchodná", ZipCode = "987654" };

            var option1 = new Option { Id = 1, Label = "Spravna volba", Time = new DateTime(2022, 10, 12), Address = addressBrno };

            var meetupSlopanie = new Meetup { Id = 1, Name = "Slopanie", OptionsFrom = new DateTime(2022, 10, 9), OptionsTo = new DateTime(2022, 10, 18), Owner = userJohan, Type = MeetupType.Drinking, Options = new List<Option> { option1 }, Logo = new byte[] { 0xFE, 0xDC, 0xEA } };

            var johanSlopanie = new UserMeetup { FirstId = userJohan.Id, SecondId = meetupSlopanie.Id, State = StateEnum.Accepted, DateInvited = DateTime.Now };
            var matysSlopanie = new UserMeetup { FirstId = userMatys.Id, SecondId = meetupSlopanie.Id, State = StateEnum.Accepted, DateInvited = DateTime.Now };
            var davidSlopanie = new UserMeetup { FirstId = userDavid.Id, SecondId = meetupSlopanie.Id, State = StateEnum.Accepted, DateInvited = DateTime.Now };

            meetupSlopanie.JoinedUsers = new List<UserMeetup> { johanSlopanie, matysSlopanie, davidSlopanie };


            dbContext.User.Add(userJohan);
            dbContext.User.Add(userMatys);
            dbContext.User.Add(userDavid);

            dbContext.Address.Add(addressBrno);
            dbContext.Address.Add(addressBratislava);

            dbContext.Meetup.Add(meetupSlopanie);

            dbContext.UserMeetup.Add(johanSlopanie);
            dbContext.UserMeetup.Add(matysSlopanie);
            dbContext.UserMeetup.Add(davidSlopanie);

            dbContext.SaveChanges();
        }
    }
}

