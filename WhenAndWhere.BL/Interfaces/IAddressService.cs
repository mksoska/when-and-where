using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Interfaces;

public interface IAddressService
{
    Task<List<AddressDTO>> GetAllAddresses();

    Task<AddressDTO> GetAddress(int id);

    void CreateAddress(AddressDTO addressDto);

    void UpdateAddress(AddressDTO addressDto);
    
    void DeleteAddress(int id);
}
