using OjoREGED.BLL.DTOs;
using OjoREGED.BLL.Interfaces;
using OjoREGED.DAL;
using OjoREGED.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace OjoREGED.BLL
{
    public class SubscriptionLevelBLL : ISubscriptionBLL
    {
        private readonly Isubscription _subscription;
        public SubscriptionLevelBLL()
        {
            _subscription = new SubscriptionDAL();

        }

        public IEnumerable<SubscriptionLevelDTO> GetAllSubs()
        {
            List<SubscriptionLevelDTO> SubscripDTOs = new List<SubscriptionLevelDTO>();
            var SubscriptionTemp = _subscription.GetAll();
            foreach (var employ in SubscriptionTemp)
            {
                SubscripDTOs.Add(new SubscriptionLevelDTO
                {
                    Subscription_ID = employ.Subscription_ID,
                    Name = employ.Name,
                    Price = employ.Price,
                });

            }
            return SubscripDTOs;
        }

        public void UpdateSubscription(SubscriptionUpdate subs)
        {
            if (subs.Subscription_ID <= 0)
            {
                throw new ArgumentException("Subscription level is required");
            }
            if (subs.Customer_ID <= 0)
            {
                throw new ArgumentException("Customer ID is required");
            }
            try
            {
                //var newsubs = new SubscriptionUpdate
                //{
                //    Customer_ID = Customer_ID.Customer_ID,
                //    Subscription_ID = subcriptions_level.Subscription_ID,
                //};
                _subscription.UpdateSubscription(subs.Subscription_ID, subs.Customer_ID);

            }
            catch (System.Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
