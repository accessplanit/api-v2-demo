using System.Globalization;

namespace CommercialSite.WebApp.App
{
    public static class DateFormatting
    {
        public const string DateTimeFormat = "dd MMM yyyy HH:mm";

        /// <summary>
        /// Formats a date string returned by the API (e.g. 2026-10-13T17:06:47.363) for display.
        /// Returns the original text if it can't be parsed, and an empty string for null.
        /// </summary>
        public static string Format(string? value)
            => DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
                ? date.ToString(DateTimeFormat, CultureInfo.InvariantCulture)
                : value ?? string.Empty;
    }
}
