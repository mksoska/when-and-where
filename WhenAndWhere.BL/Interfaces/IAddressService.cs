using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Interfaces;

public interface IAddressService
{
    Task<AddressDTO> GetAddress(int id);

    Task CreateAddress(AddressDTO addressDto);

    Task UpdateAddress(AddressDTO addressDto);
    
    Task DeleteAddress(int id);
}
