using OjoREGED.BLL.DTOs;
using System.Collections.Generic;

namespace OjoREGED.BLL.Interfaces
{
    public interface ICustomerBLL
    {
        int InsertCustomer(CustomerCreateDTO Customer);
        int AddAddress(AddressesDTO addresscustomer);
        IEnumerable<CustomerLoginDTO> CustomerLogin(CustomerLogin customerLogin);
        IEnumerable<CustomerDTO> CustomerGetByID(int id);
        IEnumerable<AddressesDTO> AddressGetByID(int id);
        IEnumerable<OrderPlacedDTO> custGetOrderByCustomeromergetbyid(int id);

        void AddBooking(InsertBookingSP insertbook);


    }
}
