using FluentAssertions;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.DTO.Filter;
using WhenAndWhere.Infrastructure.EFCore.Query;

namespace WhenAndWhere.BL.Tests;

public class QueryObjectTests : TestContextInitializer
{
    [Fact]
    public void UserNameFilterTest()
    {
        var userDto = new UserDTO
        {
            Id = 3,
            Name = "Jozef",
            Surname = "Bohdan",
            Email = "and@hob.it",
            Avatar = new byte[] {0xAA, 0xAA, 0xAA},
            PhoneNumber = "0011223344"
        };

        var queryFilterDto = new QueryFilterDto<UserDTO>
        {
            Values = userDto,
            WhereColumns = new List<string> {"Name"},
            RequestedPageNumber = 0,
            PageSize = 10,
            SortCriteria = null,
            SortAscending = true,
            SelectColumns = null
        };

        var queryObject = new UserQueryObject(mapper, new EntityFrameworkQuery<User>(dbContext));
        var actual = queryObject.ExecuteQuery(queryFilterDto);

        actual.Items.Should().HaveCount(2);
    }

    [Fact]
    public void UserNameOrderTest()
    {
        var userDto = new UserDTO
        {
            Id = 3,
            Name = "Jozef",
            Surname = "Bohdan",
            Email = "and@hob.it",
            Avatar = new byte[] {0xAA, 0xAA, 0xAA},
            PhoneNumber = "0011223344"
        };

        var queryFilterDto = new QueryFilterDto<UserDTO>
        {
            Values = userDto,
            WhereColumns = new List<string>(),
            RequestedPageNumber = 0,
            PageSize = 10,
            SortCriteria = "Name",
            SortAscending = true,
            SelectColumns = null
        };

        var expected = new List<UserDTO>
        {
            new()
            {
                Id = 3,
                Name = "Eugen",
                Surname = "Patrovic",
                Email = "aj@repujem.trosku",
                Avatar = new byte[] {0xAA, 0xAA, 0xAA},
                PhoneNumber = "0011223366"
            },
            new()
            {
                Id = 1,
                Name = "Jozef",
                Surname = "Bohdan",
                Email = "and@hob.it",
                Avatar = new byte[] {0xAA, 0xAA, 0xAA},
                PhoneNumber = "0011223344"
            },
            new()
            {
                Id = 2,
                Name = "Jozef",
                Surname = "Patrovic",
                Email = "cerstvy@vzduch.chyba",
                Avatar = new byte[] {0xAA, 0xAA, 0xAA},
                PhoneNumber = "0011223355"
            }
        };

        var queryObject = new UserQueryObject(mapper, new EntityFrameworkQuery<User>(dbContext));
        var actual = queryObject.ExecuteQuery(queryFilterDto);

        actual.Items.Should().BeEquivalentTo(expected);
    }
}