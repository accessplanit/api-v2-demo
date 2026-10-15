namespace Models
{
    public class BillingTarget
    {
        // Serialized by the API as a string, e.g. "User" or "Company"
        public string Type { get; set; }

        public string ID { get; set; }

        public string Name { get; set; }

        public bool TaxExempt { get; set; }
    }
}
