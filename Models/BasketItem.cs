
namespace Models
{
    public class BasketItem
    {
        public int BasketItemID { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public decimal UnitNet { get; set; }

        public decimal UnitTax { get; set; }

        public decimal UnitGross { get; set; }

        public string CurrencySymbol { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<BasketItemDiscount> AppliedDiscounts { get; set; }

        public IEnumerable<BasketItemUser> AvailableUsers { get; set; }

        public IEnumerable<BasketItemUser> AssignedUsers { get; set; }
    }
}
