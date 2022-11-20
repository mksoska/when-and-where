using AutoMapper;
using FluentAssertions;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore;
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
                    Name = "Jozo",
                    Surname = "Beznadejny",
                    Email = "jozko@coso.mnou",
                    PhoneNumber = "1234567890",
                    Avatar = new byte[] {0xFE, 0xDC, 0xEA}
                },
                new()
                {
                    Id = 10,
                    Name = "Milan",
                    Surname = "Beznadejny",
                    Email = "milanko@byvaloaj.lepsie",
                    PhoneNumber = "1234567891",
                    Avatar = new byte[] {0xFE, 0xDC, 0xEA}
                }
            });

        var service = new UserService(_userRepositoryMock.Object, _mapper);
        var actual = await service.GetAll();

        actual.Should().HaveCount(2);
            
        _userRepositoryMock.Verify(x => x.GetAll(), Times.Once());
    }
}