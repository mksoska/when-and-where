using System;
using AutoMapper;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class AddressService : GenericService<AddressDTO, Address>
{
    public AddressService(IRepository<Address> repository, IMapper mapper, 
        QueryObjectGeneric<AddressDTO, Address> queryObject) : base(repository, mapper, queryObject)
    {
    }
}
