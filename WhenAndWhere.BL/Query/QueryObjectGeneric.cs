using System.Linq.Expressions;
using AutoMapper;
using Castle.Core.Internal;
using WhenAndWhere.BL.Filter;
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

        foreach (var col in filter.WhereColumns) {
            var propType = filter.Values.GetType().GetProperty(col).PropertyType;
            var propValue = filter.Values.GetType().GetProperty(col).GetValue(filter.Values);

            ParameterExpression paramExpr = Expression.Parameter(propType, "obj");
            
            LambdaExpression lambdaExpr = Expression.Lambda(
                Expression.Equal(
                    paramExpr,
                    Expression.Constant(propValue)
                ),
                new List<ParameterExpression>() { paramExpr }
            );

            Func<object, bool> predicate = obj => obj == col;
            query = (IQuery<TEntity>?) query.GetType().GetMethod("Where")
                .MakeGenericMethod(propType)
                .Invoke(query, new object[] {lambdaExpr, col});
        }
        
        //TODO: Page size

        if (!string.IsNullOrWhiteSpace(filter.SortCriteria)) {
            query = query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
        }

        if (filter.RequestedPageNumber.HasValue) {
            query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
        }

        if (filter.SelectColumns != null && filter.SelectColumns.Any()) {
            query = query.Select(filter.SelectColumns);    
        }

        var items = mapper.Map<IEnumerable<TEntityDto>>(query.Execute());

        return new QueryResultDto<TEntityDto>
        {
            Items = items,
            PageSize = filter.PageSize,
            RequestedPageNumber = filter.RequestedPageNumber,
            TotalItemsCount = items.Count()
        };
    }
}