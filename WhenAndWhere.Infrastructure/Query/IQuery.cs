﻿using System.Linq.Expressions;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.Infrastructure.Query;

public interface IQuery<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// Adds a possiblity to filter the result
    /// </summary>
    IQuery<TEntity> Where<T>(Expression<Func<T, bool>> predicate, string columnName) where T : IComparable<T>;

    /// <summary>
    /// Adds a specified sort criteria to the query.
    /// </summary>
    IQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>;

    /// <summary>
    /// Adds a posibility to paginate the result
    /// </summary>
    IQuery<TEntity> Page(int pageToFetch, int pageSize = 20);

    /// <summary>
    /// Adds a posibility to select attributes from query
    /// </summary>
    IQuery<TEntity> Select(params string[] columnNames);

    /// <summary>
    /// Executes the query and returns the results.
    /// </summary>
    IEnumerable<TEntity> Execute();
}