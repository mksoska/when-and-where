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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Address> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAddress(AddressDTO addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);
            _unitOfWork.AddressRepository.Insert(address);
            await _unitOfWork.Commit();
        }

        public Address UpdateAddress(AddressDTO addressDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAdress(int id)
        {
            throw new NotImplementedException();
        }
    }
}
