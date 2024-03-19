using OjoREGED.BO;
using System;

public class Employee_Schedule
{
    public Employee_Schedule()
    {
        //this.OrderPlaceds = new List<Order_Placed>();
    }

    public int Employee_Schedule_ID { get; set; }
    public int Employee_ID { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public int Max_Order { get; set; }
    public int Order_Scheduled { get; set; }

    public Employee Employee { get; set; }
    public Order_Placed OrderPlaceds { get; set; }
}