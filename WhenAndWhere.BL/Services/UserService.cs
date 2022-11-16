using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.BL.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<UserDTO>> GetAllUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAll();
        return _mapper.Map<List<UserDTO>>(users);
    }

    public async Task<UserDTO> GetUser(int id)
    {
        var user = await _unitOfWork.UserRepository.GetById(id);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task CreateUser(UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        _unitOfWork.UserRepository.Insert(user);
        await _unitOfWork.Commit();
    }

    public async Task UpdateUser(UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.Commit();
    }

    public async Task DeleteUser(int id)
    {
        _unitOfWork.UserRepository.Delete(id);
        await _unitOfWork.Commit();
    }
}