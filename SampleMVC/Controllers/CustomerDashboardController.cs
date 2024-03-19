using Microsoft.AspNetCore.Mvc;
using OjoREGED.BLL.DTOs;
using OjoREGED.BLL.Interfaces;
using SampleMVC.ViewModels;
using System.Text.Json;

namespace SampleMVC.Controllers
{

    public class CustomerDashboardController : Controller

    {
        private readonly ICustomerBLL _customerBLL;
        private readonly ISubscriptionBLL _subsBLL;
        private readonly IemployeeBLL _employeeBLL;
        public CustomerDashboardController(ICustomerBLL customerBLL, ISubscriptionBLL subsBLL, IemployeeBLL employeeBLL)
        {
            _customerBLL = customerBLL;
            _subsBLL = subsBLL;
            _employeeBLL = employeeBLL;
        }
        public IActionResult Index()
        {
            var userDtoJson = HttpContext.Session.GetString("user");
            var userDtoList = JsonSerializer.Deserialize<List<CustomerLoginDTO>>(userDtoJson);
            var subscriptions = _subsBLL.GetAllSubs();
            var viewModel = new SubscriptionViewModel
            {
                Subscriptions = subscriptions
            };
            if (userDtoList != null && userDtoList.Count > 0)
            {
                // Assuming you want to display the first user's information
                var firstUserDto = userDtoList[0];
                ViewBag.Message = $"Welcome {firstUserDto.First_Name} {firstUserDto.Last_Name}";
                var user = _customerBLL.CustomerGetByID(firstUserDto.Customer_ID).FirstOrDefault();
                ViewBag.City = user?.AddressesDTO.City;
                ViewBag.Subscription = user.SubcriptionsLevel.Name;

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }



            return View(viewModel);

        }

        public IActionResult AddAddress()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAddress(AddressesDTO addresses)
        {
            try
            {
                var userDtoJson = HttpContext.Session.GetString("user");
                var userDtoList = JsonSerializer.Deserialize<List<CustomerLoginDTO>>(userDtoJson);
                var firstUserDto = userDtoList[0];
                var customerId = firstUserDto.Customer_ID;

                // Set the customer ID for the address
                addresses.Customer_ID = customerId;
                _customerBLL.AddAddress(addresses);

            }
            catch (Exception)
            {
                return View(addresses);
            }

            return RedirectToAction("Index");

        }

        public IActionResult CustomerProfile()
        {
            var userDtoJson = HttpContext.Session.GetString("user");
            var userDtoList = JsonSerializer.Deserialize<List<CustomerLoginDTO>>(userDtoJson);
            var firstUserDto = userDtoList[0];
            var customer = _customerBLL.CustomerGetByID(firstUserDto.Customer_ID).FirstOrDefault();



            return View(customer);
        }

        [HttpPost]
        public IActionResult CustomerProfile(CustomerProfileViewModel viewModel)
        {
            try
            {
                _customerBLL.AddAddress(viewModel.Address);

            }
            catch (Exception)
            {
                return View(viewModel);
            }

            return RedirectToAction("Index");

        }

        public IActionResult AddSubscription()
        {
            var userDtoJson = HttpContext.Session.GetString("user");
            var userDtoList = JsonSerializer.Deserialize<List<CustomerLoginDTO>>(userDtoJson);
            var firstUserDto = userDtoList[0];
            ViewBag.UserID = firstUserDto.Customer_ID;
            var subscriptions = _subsBLL.GetAllSubs();

            return View(subscriptions);
        }

        [HttpPost]
        public IActionResult AddSubscription(SubscriptionUpdate subs)
        {
            try
            {
                subs.Subscription_ID = int.Parse(Request.Form["subscription"]);
                _subsBLL.UpdateSubscription(subs);

            }
            catch (Exception)
            {
                return View(subs);
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddBooking()
        {
            IEnumerable<employeeDTO> allEmployees = _employeeBLL.GetDataEmployee();
            ViewBag.AllEmployee = allEmployees.Where(emp => emp.EmployeeSchedules.Status != "Penuh");

            return View();
        }
        [HttpPost]
        public IActionResult AddBooking(InsertBookingSP book)
        {
            try
            {
                var userDtoJson = HttpContext.Session.GetString("user");
                var userDtoList = JsonSerializer.Deserialize<List<CustomerLoginDTO>>(userDtoJson);
                var firstUserDto = userDtoList[0];
                book.Customer_ID = firstUserDto.Customer_ID;
                _customerBLL.AddBooking(book);

            }
            catch (Exception)
            {
                return View(book);
            }
            return RedirectToAction("Index");
        }

        public IActionResult BookHistory()
        {
            var userDtoJson = HttpContext.Session.GetString("user");
            var userDtoList = JsonSerializer.Deserialize<List<CustomerLoginDTO>>(userDtoJson);
            var firstUserDto = userDtoList[0];

            var BookHistory = _customerBLL.custGetOrderByCustomeromergetbyid(firstUserDto.Customer_ID);
            return View(BookHistory);
        }
    }
}
