using System;
namespace WhenAndWhere.Infrastructure.EFCore.ExpressionHelpers
{
    public class SelectEntityAttributes<TEntity> where TEntity : class, new()
    {
        private List<string> columnNames = new();

        public SelectEntityAttributes(List<string> selector)
        {
            columnNames = selector;
        }

        public TEntity InstantiateNewEntity(TEntity entity)
        {
            var result = new TEntity();
            foreach (var col in columnNames)
            {
                var colValue = entity.GetType().GetProperty(col).GetValue(entity);
                result.GetType().GetProperty(col).SetValue(result, colValue);
            }
            return result; 
        }
    }
}

