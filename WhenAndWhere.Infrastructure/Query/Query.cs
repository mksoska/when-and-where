using System.Linq.Expressions;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.Infrastructure.Query;

public abstract class Query<TEntity> : IQuery<TEntity> where TEntity : class, IEntity, new()
{
    public List<(Expression expression, Type argumentType, string columnName)> WherePredicate { get; set; } = new();
    public (string tableName, bool isAscending, Type argumentType)? OrderByContainer { get; set; }
    public (int PageToFetch, int PageSize)? PaginationContainer { get; set; }
    public List<string> SelectContainer { get; set; }

    public IQuery<TEntity> Page(int pageToFetch, int pageSize = 10)
    {
        PaginationContainer = (pageToFetch, pageSize);
        return this;
    }

    public IQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>
    {
        OrderByContainer = (columnName, ascendingOrder, typeof(T));
        return this;
    }

    // 1st argument -> table name
    public IQuery<TEntity> Where<T>(Expression<Func<T, bool>> predicate, string columnName) where T : IComparable<T>
    {
        WherePredicate.Add((predicate, typeof(T), columnName));
        return this;
    }

    public IQuery<TEntity> Select<T>(Expression<Func<T, int, object>> selector, params string[] columnNames) where T : class
    {
        SelectContainer = columnNames.ToList();
        return this;
    }

    public abstract IEnumerable<TEntity> Execute();
}