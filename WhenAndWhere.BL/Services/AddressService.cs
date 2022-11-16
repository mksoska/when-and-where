using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.BL.Services;

public class AddressService : IAddressService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AddressDTO>> GetAllAddresses()
    {
        var addresses = await _unitOfWork.AddressRepository.GetAll();
        return _mapper.Map<List<AddressDTO>>(addresses);
    }


    public async Task<AddressDTO> GetAddress(int id)
    {
        var address = await _unitOfWork.AddressRepository.GetById(id);
        return _mapper.Map<AddressDTO>(address);
    }

    public async Task CreateAddress(AddressDTO addressDto)
    {
        var address = _mapper.Map<Address>(addressDto);
        _unitOfWork.AddressRepository.Insert(address);
        await _unitOfWork.Commit();
    }

    public async Task UpdateAddress(AddressDTO addressDto)
    {
        var address = _mapper.Map<Address>(addressDto);
        _unitOfWork.AddressRepository.Update(address);
        await _unitOfWork.Commit();
    }

    public async Task DeleteAddress(int id)
    {
        _unitOfWork.AddressRepository.Delete(id);
        await _unitOfWork.Commit();
    }
}
