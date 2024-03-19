using System.Collections.Generic;

namespace OjoREGED.BO
{
    public class Order_Status
    {
        public Order_Status()
        {
            this.OrderDetails = new List<Order_Detail>();
        }

        public int Order_status { get; set; }
        public string Status_Description { get; set; }

        public List<Order_Detail> OrderDetails { get; set; }
    }
}
