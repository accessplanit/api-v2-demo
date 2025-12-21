namespace Constants
{
    public enum QueryOperations
    {
        Select,
        Filter,
        OrderBy
    }

    public static class QueryOperationsExtensions
    {
        const string Select = "$select";
        const string Filter = "$filter";
        const string OrderBy = "$orderby";

        public static string ToQueryString(this QueryOperations filter)
        {
            {
                return filter switch
                {
                    QueryOperations.Select => Select,
                    QueryOperations.Filter => Filter,
                    QueryOperations.OrderBy => OrderBy,
                    _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null)
                };
            }
        }
    }
}
