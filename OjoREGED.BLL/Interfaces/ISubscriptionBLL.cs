using OjoREGED.BLL.DTOs;
using System.Collections.Generic;

namespace OjoREGED.BLL.Interfaces
{
    public interface ISubscriptionBLL
    {
        IEnumerable<SubscriptionLevelDTO> GetAllSubs();

        void UpdateSubscription(SubscriptionUpdate subs);
    }
}
