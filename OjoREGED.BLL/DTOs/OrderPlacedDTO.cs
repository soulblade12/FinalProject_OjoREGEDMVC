using OjoREGED.BO;
using System;

namespace OjoREGED.BLL.DTOs
{
    public class OrderPlacedDTO
    {
        public int Order_ID { get; set; }
        public DateTime Time_Placed { get; set; }
        public int Customer_ID { get; set; }
        public int Employee_Schedule_ID { get; set; }

        public Customer Customer { get; set; }
        public Employee_Schedule EmployeeSchedule { get; set; }
        public Order_Detail OrderDetails { get; set; }
        public Pickup Pickups { get; set; }
    }
}
