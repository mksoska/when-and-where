using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Interfaces
{
    public interface IAddressService
    {
        Address CreateAddress(AddressDTO address);
    }
}
