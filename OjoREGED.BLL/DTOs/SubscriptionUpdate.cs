using System.ComponentModel.DataAnnotations;

namespace OjoREGED.BLL.DTOs
{
    public class SubscriptionUpdate
    {
        [Required]
        public int Subscription_ID { get; set; }
        [Required]
        public int Customer_ID { get; set; }
    }
}
