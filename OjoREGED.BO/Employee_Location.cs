using System;

namespace OjoREGED.BO
{
    public class Employee_Location
    {
        public int Employee_Loc_ID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Location_Address { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Employee_ID { get; set; }
        public string Postal_Code { get; set; }

        public Employee Employee { get; set; }
    }

}
