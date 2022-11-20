using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.DTO.Filter;
using WhenAndWhere.Infrastructure.Query;

namespace WhenAndWhere.BL.Query;

public class QueryObjectGeneric<TEntityDto, TEntity> where TEntity : class, new() where TEntityDto : class
{
    private IMapper mapper;
    private IQuery<TEntity> myQuery;

    public QueryObjectGeneric(IMapper mapper, IQuery<TEntity> myQuery)
    {
        this.mapper = mapper;
        this.myQuery = myQuery;
    }

    public QueryResultDto<TEntityDto> ExecuteQuery(QueryFilterDto<TEntityDto> filter)
    {
        var query = myQuery;

        foreach (var col in filter.WhereColumns)
        {
            var propType = filter.GetType().GetProperty(col).GetType();
            var propValue = filter.GetType().GetProperty(col).GetValue(filter);
            Func<object, bool> predicate = obj => obj == col;
            query = (IQuery<TEntity>?)query.GetType().GetMethod("Where")
                .MakeGenericMethod(propType)
                .Invoke(query, new object[] { predicate, col });
        }

        if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
        {
            query = query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
        }
        if (filter.RequestedPageNumber.HasValue)
        {
            query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
        }

        query = query.Select(filter.SelectColumns);

        return mapper.Map<QueryResultDto<TEntityDto>>(query.Execute());
    }
}

