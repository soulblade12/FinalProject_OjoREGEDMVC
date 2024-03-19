using OjoREGED.BLL.DTOs;

namespace SampleMVC.ViewModels
{
    public class SubscriptionViewModel
    {
        public IEnumerable<SubscriptionLevelDTO> Subscriptions { get; set; }
    }
}
