namespace Constants
{
    public enum PaginationOptions
    {
        Top,
        Skip,
        Count
    }

    public static class PaginationOptionsExtensions
    {
        public static string ToPaginationString(this PaginationOptions paginationOption)
        {
            return paginationOption switch
            {
                PaginationOptions.Top => "$top",
                PaginationOptions.Skip => "$skip",
                PaginationOptions.Count => "$count",
                _ => throw new ArgumentOutOfRangeException(nameof(paginationOption), paginationOption, null)
            };
        }
    }
}
