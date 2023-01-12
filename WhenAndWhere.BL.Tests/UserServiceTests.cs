using AutoMapper;
using FluentAssertions;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Tests;

public class UserServiceTests
{
    Mock<IRepository<User>> _userRepositoryMock;
    IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new EFCoreProfile());
    }));

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IRepository<User>>();
    }

    [Fact]
    public async Task GetAllUsers()
    {
        _userRepositoryMock
            .Setup(x => x.GetAll().Result)
            .Returns(new List<User>
            {
                new()
                {
                    Id = 9,
                    FirstName = "Jozo",
                    Surname = "Beznadejny",
                    Email = "jozko@coso.mnou",
                    PhoneNumber = "1234567890",
                    Avatar = new byte[] {0xFE, 0xDC, 0xEA}
                },
                new()
                {
                    Id = 10,
                    FirstName = "Milan",
                    Surname = "Beznadejny",
                    Email = "milanko@byvaloaj.lepsie",
                    PhoneNumber = "1234567891",
                    Avatar = new byte[] {0xFE, 0xDC, 0xEA}
                }
            });

        var service = new UserService(_userRepositoryMock.Object, _mapper, null);
        var actual = await service.GetAll();

        actual.Should().HaveCount(2);
            
        _userRepositoryMock.Verify(x => x.GetAll(), Times.Once());
    }
    
    [Fact]
    public async Task GetUserById()
    {
        var user = new User
        {
            Id = 3,
            FirstName = "Pepek",
            Surname = "Bezlepek",
            Email = "laktoza@akoze.vobec",
            PhoneNumber = "1234567892",
            Avatar = new byte[] {0xFE, 0xDC, 0xEA}
        };
        var expected = new UserDTO
        {
            Id = 3,
            FirstName = "Pepek",
            Surname = "Bezlepek",
            Email = "laktoza@akoze.vobec",
            PhoneNumber = "1234567892",
            Avatar = new byte[] {0xFE, 0xDC, 0xEA}
        };
        
        _userRepositoryMock
            .Setup(x => x.GetById(3).Result)
            .Returns(user);

        var service = new UserService(_userRepositoryMock.Object, _mapper, null);
        var actual = await service.GetById(3);

        actual.Should().BeEquivalentTo(expected);
            
        _userRepositoryMock.Verify(x => x.GetById(3), Times.Once());
    }
}