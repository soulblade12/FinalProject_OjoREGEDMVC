using OjoREGED.BO;

namespace OjoREGED.DAL.Interfaces
{
    public interface Isubscription : Icrud<Subcriptions_Level>
    {
        void UpdateSubscription(int subcriptions_level, int Customer_ID);
    }
}
