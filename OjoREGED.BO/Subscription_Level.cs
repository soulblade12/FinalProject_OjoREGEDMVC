using System.Collections.Generic;

namespace OjoREGED.BO
{
    public class Subcriptions_Level
    {
        public Subcriptions_Level()
        {
            this.Customers = new List<Customer>();
        }

        public int Subscription_ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<Customer> Customers { get; set; }
    }
}
