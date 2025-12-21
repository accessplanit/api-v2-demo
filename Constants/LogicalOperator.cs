namespace Constants
{
    public enum LogicalOperator
    {
        And,
        Or
    }

    public static class LogicalOperatorExtensions
    {
        public static string ToLogicString(this LogicalOperator _operator)
        {
            return _operator switch
            {
                LogicalOperator.And => "and",
                LogicalOperator.Or => "or",
                _ => throw new ArgumentOutOfRangeException(nameof(_operator), _operator, null)
            };
        }
    }
}
