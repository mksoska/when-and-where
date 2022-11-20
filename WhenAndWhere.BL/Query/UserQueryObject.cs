using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.DTO.Filter;
using WhenAndWhere.Infrastructure.EFCore.Query;
using WhenAndWhere.Infrastructure.Query;
using System.Linq.Expressions;

namespace WhenAndWhere.BL.Query;

public class UserQueryObject : QueryObjectGeneric<UserDTO, User>
{ 
    public UserQueryObject(IMapper mapper, IQuery<User> myQuery) : base(mapper, myQuery)
    { 
    }

    public QueryResultDto<UserDTO> UserNameFilter(string name)
    {
        var queryDto = new QueryFilterDto<UserDTO> { Values = new UserDTO { Name = name }, WhereColumns = new List<string> { "Name" } };
        return ExecuteQuery(queryDto);
    }
}