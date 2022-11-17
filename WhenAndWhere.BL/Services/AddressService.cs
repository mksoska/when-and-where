using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class AddressService : IAddressService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Address> _addressRepository;

    public AddressService(IRepository<Address> addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task<List<AddressDTO>> GetAllAddresses()
    {
        var addresses = await _addressRepository.GetAll();
        return _mapper.Map<List<AddressDTO>>(addresses);
    }

    public async Task<AddressDTO> GetAddress(int id)
    {
        var address = await _addressRepository.GetById(id);
        return _mapper.Map<AddressDTO>(address);
    }

    public void CreateAddress(AddressDTO addressDto)
    {
        var address = _mapper.Map<Address>(addressDto);
        _addressRepository.Insert(address);
    }

    public void UpdateAddress(AddressDTO addressDto)
    {
        var address = _mapper.Map<Address>(addressDto);
        _addressRepository.Update(address);
    }

    public void DeleteAddress(int id)
    {
        _addressRepository.Delete(id);
    }
}
