namespace OjoREGED.BO
{
    public class Customer
    {
        public Customer()
        {
            //this.Addresses = new List<Address>();
            //this.CustomerSubscriptions = new List<Customer_Subscription>();
            //this.OrderPlaceds = new List<Order_Placed>();
        }

        public int Customer_ID { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Telephone { get; set; }
        public int Subscription_ID { get; set; }
        public string Email_Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role_ID { get; set; }
        public string Image { get; set; }

        public Subcriptions_Level SubcriptionsLevel { get; set; }
        public Address Addresses { get; set; }
        public Customer_Subscription CustomerSubscriptions { get; set; }
        public Order_Placed OrderPlaceds { get; set; }
        public Role Role { get; set; }
    }
}
