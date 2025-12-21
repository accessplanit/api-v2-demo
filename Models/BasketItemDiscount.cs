
namespace Models
{
    public class BasketItemDiscount
    {
        public int RelatedBasketItemID { get; set; }

        public string Name { get; set; }

        public decimal UnitNet { get; set; }

        public decimal UnitTax { get; set; }

        public decimal UnitGross { get; set; }

        public string CurrencySymbol { get; set; }

        public bool Automatic { get; set; }

        public string DiscountCode { get; set; }
    }
}
