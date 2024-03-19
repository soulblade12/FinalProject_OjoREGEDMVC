using OjoREGED.BO;

namespace OjoREGED.BLL.DTOs
{
    public class CustomerDTO
    {
        public int Customer_ID { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Telephone { get; set; }
        public int Subscription_ID { get; set; }
        public string Email_Address { get; set; }
        public string Username { get; set; }
        public int Role_ID { get; set; }
        public string Image { get; set; }

        public SubscriptionLevelDTO SubcriptionsLevel { get; set; }
        public AddressesDTO AddressesDTO { get; set; }
        public CustomerSubscriptionsDTO CustomerSubscriptionsDTO { get; set; }
        public Order_Placed OrderPlaceds { get; set; }
        public Role Role { get; set; }
    }
}
