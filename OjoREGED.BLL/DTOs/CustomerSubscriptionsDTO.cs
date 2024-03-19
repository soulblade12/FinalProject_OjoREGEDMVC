using System;

namespace OjoREGED.BLL.DTOs
{
    public class CustomerSubscriptionsDTO
    {
        public int Customer_SubsID { get; set; }
        public int Customer_ID { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public decimal Amount { get; set; }

        public CustomerDTO Customer { get; set; }
    }
}
