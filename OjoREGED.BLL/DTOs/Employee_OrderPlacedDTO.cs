using System;

namespace OjoREGED.BLL.DTOs
{
    public class Employee_OrderPlacedDTO
    {
        public int Order_ID { get; set; }
        public DateTime Time_Placed { get; set; }
        public int Customer_ID { get; set; }
        public int Employee_Schedule_ID { get; set; }
        public int Employee_ID { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public int Max_Order { get; set; }
        public int Order_Scheduled { get; set; }
        public int OrderDetail_ID { get; set; }
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public string Order_Instruction { get; set; }
        public string Order_Img { get; set; }
        public string Status_Description { get; set; }
    }
}
