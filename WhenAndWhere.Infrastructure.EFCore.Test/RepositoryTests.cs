using System;
using Microsoft.EntityFrameworkCore;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Repository;

namespace WhenAndWhere.Infrastructure.EFCore.Test
{
    public class RepositoryTests : TestContextInitializer
    {
        [Fact]
        public void UsersAttendMeetup_RepositoryGetById_Test()
        {
            var efrepository = new EFGenericRepository<UserMeetup>(dbContext);
            var result = efrepository.GetById(2, 1);

            var ExpectedResult = dbContext.UserMeetup
                .First(um => um.FirstId == 2 && um.SecondId == 1);

            Assert.Equal(ExpectedResult, result);
        }

        [Fact]
        public void MeetupHasAddress_RepositoryGetById_Test()
        {
            var efrepository = new EFGenericRepository<Meetup>(dbContext);
            var result = efrepository.GetById(1).Options.First(o => o.Id == 1);

            var ExpectedResult = dbContext.Option
                .First(o => o.Id == 1);

            Assert.Equal(ExpectedResult, result);
        }
    }
}

