using System.Collections.Generic;

namespace OjoREGED.BO
{
    public class Role
    {
        public Role()
        {
            this.Customers = new List<Customer>();
            this.Employees = new List<Employee>();
        }

        public int Role_ID { get; set; }
        public string Role_Name { get; set; }

        public List<Customer> Customers { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
