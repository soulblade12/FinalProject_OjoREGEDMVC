using System;

namespace OjoREGED.BLL.DTOs
{
    public class EmployeeScheduleDTO
    {
        public int Employee_Schedule_ID { get; set; }
        public int Employee_ID { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public int Max_Order { get; set; }
        public int Order_Scheduled { get; set; }
    }
}
