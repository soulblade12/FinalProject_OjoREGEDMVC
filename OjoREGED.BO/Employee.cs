using System.Collections.Generic;

namespace OjoREGED.BO
{
    public class Employee
    {
        public Employee()
        {
            //this.EmployeeLocations = new List<Employee_Location>();
            //this.EmployeeSchedules = new List<Employee_Schedule>();
            this.Pickups = new List<Pickup>();
        }

        public int Employee_ID { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Telephone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public int Role_ID { get; set; }

        public Role Role { get; set; }
        public Employee_Location EmployeeLocations { get; set; }
        public Employee_Schedule EmployeeSchedules { get; set; }
        public List<Pickup> Pickups { get; set; }
    }
}
