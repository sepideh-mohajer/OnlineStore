using System.Linq.Expressions;

namespace OnlineStore.Infrastructure.Helpers
{
    public static class ExpressionHelper
    {
        public static Expression<Func<TEntity, bool>> BuildSearchExpression<TEntity>(Dictionary<string, string> searchCriteria)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var expressions = new List<Expression>();

            foreach (var criteria in searchCriteria)
            {
                var property = GetNestedPropertyExpression(parameter, criteria.Key);
                if (property != null && property.Type == typeof(string))
                {
                    var searchExpression = Expression.Call(property, "Contains", null, Expression.Constant(criteria.Value));
                    expressions.Add(searchExpression);
                }
            }

            if (expressions.Count == 0)
            {
                return e => true;
            }

            var orExpression = expressions.Aggregate(Expression.OrElse);
            return Expression.Lambda<Func<TEntity, bool>>(orExpression, parameter);
        }

        private static MemberExpression GetNestedPropertyExpression(Expression parameter, string propertyName)
        {
            var properties = propertyName.Split('.');
            Expression expression = parameter;

            foreach (var property in properties)
            {
                expression = Expression.Property(expression, property);
            }

            return expression as MemberExpression;
        }
    }
}
