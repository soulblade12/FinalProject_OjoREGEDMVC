namespace OjoREGED.BLL.DTOs
{
    public class EmployeeInsertPickup
    {
        public int Employee_ID { get; set; }
        public string Delivery_Status { get; set; }
        public decimal Delivery_Charges { get; set; }
        public decimal Tip_Amount { get; set; }
        public int Order_ID { get; set; }
    }
}
