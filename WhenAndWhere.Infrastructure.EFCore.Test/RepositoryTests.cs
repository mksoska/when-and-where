using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Repository;

namespace WhenAndWhere.Infrastructure.EFCore.Test;

public class RepositoryTests : TestContextInitializer
{
    [Fact]
    public void UsersAttendMeetup_RepositoryGetById_Test()
    {
        var efrepository = new EFGenericRepository<UserMeetup>(dbContext);
        var result = efrepository.GetById(2, 1).Result;

        var ExpectedResult = dbContext.UserMeetup
            .First(um => um.FirstId == 2 && um.SecondId == 1);

        Assert.Equal(ExpectedResult, result);
    }

    [Fact]
    public void MeetupHasAddress_RepositoryGetById_Test()
    {
        var efrepository = new EFGenericRepository<Meetup>(dbContext);
        var result = efrepository.GetById(1).Result.Options.First(o => o.Id == 1);

        var ExpectedResult = dbContext.Option
            .First(o => o.Id == 1);

        Assert.Equal(ExpectedResult, result);
    }
    
    [Fact]
    public void GetAllMeetups_Test()
    {
        var efrepository = new EFGenericRepository<Meetup>(dbContext);
        var result = efrepository.GetAll().Result;

        var ExpectedResult = dbContext.Meetup.ToList();

        Assert.Equal(ExpectedResult, result);
    }
    
    [Fact]
    public void InsertUser_Test()
    {
        var efrepository = new EFGenericRepository<User>(dbContext);
        var user = new User
        {
            Name = "Marek", 
            Surname = "Petrovicz", 
            Email = "gg@hh.ii", 
            PhoneNumber = "3333333333", 
            Avatar = new byte[] { 0xFE, 0xDC, 0xEA }
        };
        efrepository.Insert(user);
        var ExpectedUser = dbContext.User.Find(user.Id);

        Assert.Equal(ExpectedUser, user);
    }
    
    [Fact]
    public void UpdateUser_Test()
    {
        var efrepository = new EFGenericRepository<User>(dbContext);
        var user = efrepository.GetById(1).Result;
        user.PhoneNumber = "4444444444";
        efrepository.Update(user);
        var ExpectedUser = dbContext.User.Find(user.Id);

        Assert.Equal(ExpectedUser, user);
    }
    
    [Fact]
    public async Task DeleteUser_Test()
    {
        var efrepository = new EFGenericRepository<User>(dbContext);
        efrepository.Delete(3);
        await efrepository.Save();
        var user = efrepository.GetById(3).Result;
        User ExpectedUser = null;

        Assert.Equal(ExpectedUser, user);
    }
}
