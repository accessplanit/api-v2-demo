using System.Globalization;

namespace Models
{
    /// <summary>
    /// Formats money using the item's own currency instead of the server's culture.
    /// </summary>
    public static class CurrencyFormatting
    {
        public const string DefaultCurrencyCode = "GBP";

        public static string Format(decimal amount, string? currencyCode = null)
        {
            var code = string.IsNullOrWhiteSpace(currencyCode) ? DefaultCurrencyCode : currencyCode.Trim().ToUpperInvariant();

            var symbol = code switch
            {
                "GBP" => "£",
                "EUR" => "€",
                "USD" => "$",
                _ => code + " "
            };

            var sign = amount < 0 ? "-" : string.Empty;

            return $"{sign}{symbol}{Math.Abs(amount).ToString("N2", CultureInfo.InvariantCulture)}";
        }

        public static string Format(double amount, string? currencyCode = null)
            => Format((decimal)amount, currencyCode);
    }
}
