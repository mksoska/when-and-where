using FluentAssertions;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Filter;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DAL.Models;
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

        var queryObject = new QueryObjectGeneric<UserDTO, User>(mapper, new EntityFrameworkQuery<User>(dbContext));
        var actual = queryObject.ExecuteQuery(queryFilterDto);

        actual.Items.Should().HaveCount(2);
    }

    [Fact]
    public void MeetupOwnerFilterTest()
    {
        var meetupDto = new MeetupDTO { OwnerId = 1 };

        var queryFilterDto = new QueryFilterDto<MeetupDTO>
        {
            Values = meetupDto,
            WhereColumns = new List<string> { "OwnerId" },
        };

        var queryObject = new QueryObjectGeneric<MeetupDTO, Meetup>(mapper, new EntityFrameworkQuery<Meetup>(dbContext));
        var actual = queryObject.ExecuteQuery(queryFilterDto);

        var expected = new List<MeetupDTO>
        {
            mapper.Map<MeetupDTO>(dbContext.Meetup.Find(1))
        };

        actual.Items.Should().BeEquivalentTo(expected);
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

        var queryObject = new QueryObjectGeneric<UserDTO, User>(mapper, new EntityFrameworkQuery<User>(dbContext));
        var actual = queryObject.ExecuteQuery(queryFilterDto);

        actual.Items.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void UserPaginationTest()
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
            RequestedPageNumber = 2,
            PageSize = 2,
            SortCriteria = null,
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
            }
        };

        var queryObject = new QueryObjectGeneric<UserDTO, User>(mapper, new EntityFrameworkQuery<User>(dbContext));
        var actual = queryObject.ExecuteQuery(queryFilterDto);

        actual.Items.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void UserSelectTest()
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
            SortCriteria = null,
            SortAscending = true,
            SelectColumns = new[] {"Id", "Name", "Surname"}
        };

        var expected = new List<UserDTO>
        {
            new()
            {
                Id = 1,
                Name = "Jozef",
                Surname = "Bohdan",
                Email = null,
                Avatar = Array.Empty<byte>(),
                PhoneNumber = null
            },
            new()
            {
                Id = 2,
                Name = "Jozef",
                Surname = "Patrovic",
                Email = null,
                Avatar = Array.Empty<byte>(),
                PhoneNumber = null
            },
            new()
            {
                Id = 3,
                Name = "Eugen",
                Surname = "Patrovic",
                Email = null,
                Avatar = Array.Empty<byte>(),
                PhoneNumber = null
            }
        };

        var queryObject = new QueryObjectGeneric<UserDTO, User>(mapper, new EntityFrameworkQuery<User>(dbContext));
        var actual = queryObject.ExecuteQuery(queryFilterDto);

        actual.Items.Should().BeEquivalentTo(expected);
    }
}