using OjoREGED.BLL.DTOs;

namespace SampleMVC.ViewModels
{
    public class CustomerProfileViewModel
    {
        public CustomerDTO Customer { get; set; }
        public AddressesDTO Address { get; set; }
    }
}
