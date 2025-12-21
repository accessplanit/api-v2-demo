namespace Constants
{
    public enum FilterExpressions
    {
        Equals,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }

    public static class FilterExpressionsExtensions
    {
        const string Equals = "eq";
        const string NotEqual = "eq";
        const string LessThan = "lt";
        const string LessThanOrEqual = "le";
        const string GreaterThan = "gt";
        const string GreaterThanOrEqual = "ge";
        const string Contains = "contains";
        const string StartsWith = "startswith";
        const string EndsWith = "endswith";

        public static string? ToFilterExpressionString(this FilterExpressions? filter)
        {
            if(filter is not null)
            {
                return filter switch
                {
                    FilterExpressions.Equals => Equals,
                    FilterExpressions.NotEqual => NotEqual,
                    FilterExpressions.LessThan => LessThan,
                    FilterExpressions.LessThanOrEqual => LessThanOrEqual,
                    FilterExpressions.GreaterThan => GreaterThan,
                    FilterExpressions.GreaterThanOrEqual => GreaterThanOrEqual,
                    FilterExpressions.Contains => Contains,
                    FilterExpressions.StartsWith => StartsWith,
                    FilterExpressions.EndsWith => EndsWith,
                    _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null)
                };
            }

            return null;
        }

        public static string ToFilterExpressionString(this FilterExpressions filter)
        {
            return filter switch
            {
                FilterExpressions.Equals => Equals,
                FilterExpressions.NotEqual => NotEqual,
                FilterExpressions.LessThan => LessThan,
                FilterExpressions.LessThanOrEqual => LessThanOrEqual,
                FilterExpressions.GreaterThan => GreaterThan,
                FilterExpressions.GreaterThanOrEqual => GreaterThanOrEqual,
                FilterExpressions.Contains => Contains,
                FilterExpressions.StartsWith => StartsWith,
                FilterExpressions.EndsWith => EndsWith,
                _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null)
            };
        }
    }
}
