using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Interfaces
{
    public interface IAddressService
    {
        Task<Address> FindById(int id);

        Task CreateAddress(AddressDTO addressDto);

        Address UpdateAddress(AddressDTO addressDto);
        
        void DeleteAdress(int id);
    }
}
