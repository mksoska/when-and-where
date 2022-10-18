﻿using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Query;
using WhenAndWhere.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Test
{
    public class QueryTests
    {
        private readonly WhenAndWhereDBContext dbContext;

        public QueryTests()
        {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

            var dbContextOptions = new DbContextOptionsBuilder<WhenAndWhereDBContext>()
                            .UseInMemoryDatabase(databaseName: myDatabaseName)
                            .Options;

            dbContext = new WhenAndWhereDBContext(dbContextOptions);

            var userJohan = new User { Id = 1, Name = "Johan", Surname = "Kovalchuck", Email = "aa@bb.cc", PhoneNumber = "1111111111", Avatar = new byte[] { 0xAB, 0xCD, 0xEF } };
            var userMatys = new User { Id = 2, Name = "Matysku", Surname = "Popisku", Email = "dd@ee.ff", PhoneNumber = "2222222222", Avatar = new byte[] { 0xFE, 0xDC, 0xBA } };
            var userDavid = new User { Id = 3, Name = "Davit", Surname = "Maslovich", Email = "gg@hh.ii", PhoneNumber = "3333333333", Avatar = new byte[] { 0xFE, 0xDC, 0xEA } };

            var addressBrno = new Address { Id = 1, City = "Brno", Number = "9", State = "CZE", Street = "Ryšánková", ZipCode = "61300" };
            var addressBratislava = new Address { Id = 2, City = "Bratislava", Number = "7", State = "SK", Street = "Obchodná", ZipCode = "987654" };

            var option1 = new Option { Id = 1, Label = "Spravna volba", Time = new DateTime(2022, 10, 12) };

            var meetupSlopanie = new Meetup { Id = 1, Name = "Slopanie", OptionsFrom = new DateTime(2022, 10, 9), OptionsTo = new DateTime(2022, 10, 18), Owner = userJohan, Type = MeetupType.Drinking, Options = new List<Option> {option1}, Logo = new byte[] { 0xFE, 0xDC, 0xEA } };



            dbContext.User.Add(userJohan);
            dbContext.User.Add(userMatys);
            dbContext.User.Add(userDavid);

            dbContext.Address.Add(addressBrno);
            dbContext.Address.Add(addressBratislava);

            dbContext.Meetup.Add(meetupSlopanie);  

            dbContext.SaveChanges();
        }

        [Fact]
        public void UserJohanExists_QueryWhere_Test()
        {
            var efquery = new EntityFrameworkQuery<User>(dbContext);
            efquery.Where<string>(a => a == "Johan", "Name");
            var result = efquery.Execute();

            Assert.True(result.Count() == 1);

            Assert.True(result.First().Name == "Johan");
        }

        /*
        [Fact]
        public void ClassroomsThatStartWithBExists_QueryWhere_Test()
        {
            var efquery = new EntityFrameworkQuery<Classroom>(dbContext);
            efquery.Where<string>(a => a.StartsWith("B"), "Name");
            var result = efquery.Execute();

            Assert.True(result.Count() == 2);
        }

        [Fact]
        public void ClassroomsWithIDLessThan2_QueryWhere_Test()
        {
            var efquery = new EntityFrameworkQuery<Classroom>(dbContext);
            efquery.Where<int>(a => a < 3, "Id");
            var result = efquery.Execute();

            Assert.True(result.Count() == 2);
        }

        [Fact]
        public void ClassroomsOrderedAscending_QueryOrderBy_Test()
        {
            var efquery = new EntityFrameworkQuery<Classroom>(dbContext);
            efquery.OrderBy<int>("Id", true);
            var result = efquery.Execute()
                .Select(a => a.Id)
                .ToList();

            var ExpectedResult = dbContext.Classrooms
                .Select(a => a.Id)
                .OrderBy(a => a)
                .ToList();

            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void ClassroomsOrderedDescending_QueryOrderBy_Test()
        {
            var efquery = new EntityFrameworkQuery<Classroom>(dbContext);
            efquery.OrderBy<int>("Id", false);
            var result = efquery.Execute()
                .Select(a => a.Id)
                .ToList();

            var ExpectedResult = dbContext.Classrooms
                .Select(a => a.Id)
                .OrderByDescending(a => a)
                .ToList();

            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void StudentsSimplePagination_QueryPagination_Test()
        {
            var efquery = new EntityFrameworkQuery<Student>(dbContext);
            efquery.Page(3, 3);
            var result = efquery.Execute()
                .Select(a => a.Id)
                .ToList();

            var ExpectedResult = dbContext.Students
                .Skip(6)
                .Take(3)
                .Select(a => a.Id)
                .ToList();

            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void QueryAdvancedTest1()
        {
            var efquery = new EntityFrameworkQuery<Student>(dbContext);
            efquery.Where<int>(a => a < 7, "Id");
            efquery.OrderBy<string>("Name", true);
            efquery.Page(1, 4);
            var result = efquery.Execute().ToList();

            var ExpectedResult = dbContext.Students
                .Where(a => a.Id < 7)
                .OrderBy(a => a.Name)
                .Take(4)
                .ToList();

            Assert.Equal(ExpectedResult, result);
        }
        */
    }
}
