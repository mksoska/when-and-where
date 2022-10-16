using WhenAndWhere.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WhenAndWhere.Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Adds a possiblity to filter the result
        /// </summary>
        IQuery<TEntity> Where<T>(Expression<Func<T, bool>> rootPredicate, string columnName) where T : IComparable<T>;

        /// <summary>
        /// Adds a specified sort criteria to the query.
        /// </summary>
        IQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>;

        /// <summary>
        /// Adds a posibility to paginate the result
        /// </summary>
        IQuery<TEntity> Page(int pageToFetch, int pageSize = 20);

        /// <summary>
        /// Executes the query and returns the results.
        /// </summary>
        IEnumerable<TEntity> Execute();
    }
}
