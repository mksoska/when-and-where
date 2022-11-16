using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Interfaces;

public interface IUserService
{
    Task<UserDTO> GetUser(int id);

    Task CreateUser(UserDTO userDto);

    Task UpdateUser(UserDTO userDto);

    Task DeleteUser(int id);
}