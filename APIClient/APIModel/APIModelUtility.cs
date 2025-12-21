using System.Linq.Expressions;

namespace APIClient.APIModel
{
    public static class APIModelUtility
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            else if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
            {
                // Handle cases where a value type is boxed (e.g., converting int to object)
                return operand.Member.Name;
            }

            throw new ArgumentException("Expression is not a valid member expression.");
        }
    }
}
