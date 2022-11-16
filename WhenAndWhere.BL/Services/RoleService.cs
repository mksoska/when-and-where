using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.BL.Services;

public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<RoleDTO>> GetAllRoles()
    {
        var roles = await _unitOfWork.RoleRepository.GetAll();
        return _mapper.Map<List<RoleDTO>>(roles);
    }

    public async Task<RoleDTO> GetRole(int id)
    {
        var role = await _unitOfWork.RoleRepository.GetById(id);
        return _mapper.Map<RoleDTO>(role);
    }

    public async Task CreateRole(RoleDTO roleDto)
    {
        var role = _mapper.Map<Role>(roleDto);
        _unitOfWork.RoleRepository.Insert(role);
        await _unitOfWork.Commit();
    }

    public async Task UpdateRole(RoleDTO roleDto)
    {
        var role = _mapper.Map<Role>(roleDto);
        _unitOfWork.RoleRepository.Update(role);
        await _unitOfWork.Commit();
    }

    public async Task DeleteRole(int id)
    {
        _unitOfWork.RoleRepository.Delete(id);
        await _unitOfWork.Commit();
    }
}
