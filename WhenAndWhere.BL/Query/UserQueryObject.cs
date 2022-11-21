using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.DTO.Filter;
using WhenAndWhere.Infrastructure.Query;

namespace WhenAndWhere.BL.Query;

public class UserQueryObject : QueryObjectGeneric<UserDTO, User>
{
    public UserQueryObject(IMapper mapper, IQuery<User> myQuery) : base(mapper, myQuery)
    {
    }
}