using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Query;

namespace WhenAndWhere.Infrastructure.EFCore.Test;

public class QueryTests : TestContextInitializer
{
    [Fact]
    public void UserJohanExists_QueryWhere_Test()
    {
        var efquery = new EntityFrameworkQuery<User>(dbContext);
        efquery.Where<string>(a => a == "Johan", "Name");
        var result = efquery.Execute();

        Assert.True(result.Count() == 1);

        Assert.True(result.First().Name == "Johan");
    }

    [Fact]
    public void UsersWithLetter_ch_Exists_QueryWhere_Test()
    {
        var efquery = new EntityFrameworkQuery<User>(dbContext);
        efquery.Where<string>(a => a.Contains("ch"), "Surname");
        var result = efquery.Execute();

        Assert.True(result.Count() == 2);
    }

    [Fact]
    public void UsersOrderedAscending_QueryOrderBySurname_Test()
    {
        var efquery = new EntityFrameworkQuery<User>(dbContext);
        efquery.OrderBy<string>("Surname", true);
        var result = efquery.Execute()
            .Select(a => a.Surname)
            .ToList();

        var ExpectedResult = dbContext.User
            .Select(a => a.Surname)
            .OrderBy(a => a)
            .ToList();

        Assert.Equal(ExpectedResult, result);
    }

    [Fact]
    public void UsersOrderedDescending_QueryOrderBySurname_Test()
    {
        var efquery = new EntityFrameworkQuery<User>(dbContext);
        efquery.OrderBy<int>("Id", false);
        var result = efquery.Execute()
            .Select(a => a.Id)
            .ToList();

        var ExpectedResult = dbContext.User
            .Select(a => a.Id)
            .OrderByDescending(a => a)
            .ToList();

        Assert.Equal(ExpectedResult, result);
    }

    [Fact]
    public void UsersSimplePagination_QueryPagination_Test()
    {
        var efquery = new EntityFrameworkQuery<User>(dbContext);
        efquery.Page(1, 3);
        var result = efquery.Execute()
            .Select(a => a.Id)
            .ToList();

        var ExpectedResult = dbContext.User
            .Skip(0)
            .Take(3)
            .Select(a => a.Id)
            .ToList();

        Assert.Equal(ExpectedResult, result);
    }

    [Fact]
    public void UsersAdvancedPagination_QueryPagination_Test()
    {
        var efquery = new EntityFrameworkQuery<User>(dbContext);
        efquery.Page(2, 2);
        var result = efquery.Execute()
            .Select(a => a.Id)
            .ToList();

        var ExpectedResult = dbContext.User
            .Skip(2)
            .Take(1)
            .Select(a => a.Id)
            .ToList();

        Assert.Equal(ExpectedResult, result);
    }

    [Fact]
    public void MeetupOwnersSelection_QuerySelect_Test()
    {
        var efquery = new EntityFrameworkQuery<Meetup>(dbContext);
        efquery.Select("Id", "OwnerId");
        var result = efquery.Execute()
            .Select(m => m.OwnerId)
            .ToList();
            
        var ExpectedResult = dbContext.Meetup
            .Select(m => m.OwnerId)
            .ToList();

        Assert.Equal(ExpectedResult, result);
    }
}