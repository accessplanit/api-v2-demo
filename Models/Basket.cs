using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Basket
    {
        public int BasketID { get; set; }

        public string BookingUserID { get; set; }

        public string UniqueBasketID { get; set; }

        public IEnumerable<BasketItem> Items { get; set; }

        public string LatestErrorMessage { get; set; }
    }
}
