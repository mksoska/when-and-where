using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.DTO.Filter;
using WhenAndWhere.Infrastructure.EFCore.Query;

namespace WhenAndWhere.BL.Query;

public class UserQueryObject
{
    private IMapper mapper;

    private EntityFrameworkQuery<User> myQuery;

    public UserQueryObject(IMapper mapper, EntityFrameworkQuery<User> myQuery)
    {
        this.mapper = mapper;
        this.myQuery = myQuery;
    }

    public QueryResultDto<UserDTO> ExecuteQuery(UserNameFilterDTO filter)
    {
        var query = myQuery.Where<string>(x => x == filter.Name, "Name");
        if (!string.IsNullOrWhiteSpace(filter.SortCriteria)) {
            query = query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
        }
        if (filter.RequestedPageNumber.HasValue)
        {
            query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
        }

        return mapper.Map<QueryResultDto<UserDTO>>(query.Execute());
    }
}