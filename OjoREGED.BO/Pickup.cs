using System;

namespace OjoREGED.BO
{
    public class Pickup
    {
        public Pickup()
        {
            //this.BillsIds = new List<Bills_ID>();
        }

        public int Pickup_ID { get; set; }
        public int Employee_ID { get; set; }
        public DateTime Pickup_Time { get; set; }
        public string Delivery_Status { get; set; }
        public int Order_ID { get; set; }

        public Employee Employee { get; set; }
        public Order_Placed OrderPlaced { get; set; }
        public Bills_ID BillsIds { get; set; }
    }
}
