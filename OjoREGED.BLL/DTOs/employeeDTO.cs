using OjoREGED.BO;

namespace OjoREGED.BLL.DTOs
{
    public class employeeDTO
    {
        public int Employee_ID { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Telephone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role_ID { get; set; }

        public Employee_Location EmployeeLocations { get; set; }
        public Employee_Schedule EmployeeSchedules { get; set; }
    }
}
