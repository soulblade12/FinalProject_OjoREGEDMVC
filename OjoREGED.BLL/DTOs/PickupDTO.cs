using OjoREGED.BO;
using System;

namespace OjoREGED.BLL.DTOs
{
    public class PickupDTO
    {
        public int Pickup_ID { get; set; }
        public int Employee_ID { get; set; }
        public DateTime Pickup_Time { get; set; }
        public string Delivery_Status { get; set; }
        public int Order_ID { get; set; }

        public Bills_ID BillsIds { get; set; }
    }
}
