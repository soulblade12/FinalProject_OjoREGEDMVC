using System;

namespace OjoREGED.BO
{
    public class Order_Placed
    {
        public Order_Placed()
        {
            //this.OrderDetails = new List<Order_Detail>();
            //this.Pickups = new List<Pickup>();
        }

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
