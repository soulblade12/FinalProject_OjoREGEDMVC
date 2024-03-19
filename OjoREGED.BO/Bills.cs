using System;

namespace OjoREGED.BO
{
    public class Bills_ID
    {
        public int bills_ID { get; set; }
        public decimal Delivery_Charges { get; set; }
        public decimal Tip_Amount { get; set; }
        public int Pickup_ID { get; set; }
        public DateTime BillDate { get; set; }

        public Pickup Pickup { get; set; }
    }
}
