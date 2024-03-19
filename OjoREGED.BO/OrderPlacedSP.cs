using System;

namespace OjoREGED.BO
{
    public class OrderPlacedSP
    {
        public int Order_ID { get; set; }
        public DateTime Time_Placed { get; set; }
        public int Customer_ID { get; set; }
        public int Employee_Schedule_ID { get; set; }
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public string Order_Instruction { get; set; }
    }
}
