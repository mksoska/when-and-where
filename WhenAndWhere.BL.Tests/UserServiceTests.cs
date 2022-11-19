using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.BL.Services;
using AutoMapper;
using FluentAssertions;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.EFCore;

namespace WhenAndWhere.BL.Tests
{
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
        public async Task GetUserExisting()
        {
            var expected = new User
            {
                Id = 9,
                Name = "Jozo",
                Surname = "Beznadejny",
                Email = "jozko@coso.mnou",
                PhoneNumber = "1234567890",
                Avatar = new byte[] { 0xFE, 0xDC, 0xEA }
            };

            _userRepositoryMock
                .Setup(x => x.GetAll().Result)
                .Returns(new List<User>
                {
                    expected,
                });

            var service = new UserService(_userRepositoryMock.Object, _mapper);
            var actual = await service.GetAll();

            actual.Should().HaveCount(1);
        }
    }
}
