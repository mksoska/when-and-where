using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.BL.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _addressRepository;
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public Address CreateAddress(AddressDTO addressDTO)
        {
            var address = _mapper.Map<Address>(addressDTO);
            _addressRepository.Insert(address);
            return address;
        }
    }
}
