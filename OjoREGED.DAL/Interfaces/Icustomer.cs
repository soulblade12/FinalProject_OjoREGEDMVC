using OjoREGED.BO;
using System.Collections.Generic;

namespace OjoREGED.DAL.Interfaces
{
    public interface Icustomer : Icrud<Customer>
    {
        int addCustomer(Customer datacustomer);
        IEnumerable<Customer> GetcustomerByID(int id);
        IEnumerable<Address> GetAddressByID(int id);
        IEnumerable<Customer> Login(string Username, string password);

        void AddBooking(OrderPlacedSP orderplaced);

        int AddAddress(Address addresscustomer);
        IEnumerable<Order_Placed> GetOrderByCustomer(int id);

    }
}
