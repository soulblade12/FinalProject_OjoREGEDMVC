using Microsoft.AspNetCore.Mvc;
using OjoREGED.BLL.DTOs;
using OjoREGED.BLL.Interfaces;
using System.Text.Json;

namespace SampleMVC.Controllers
{
    public class EmployeeDashboardController : Controller
    {
        private readonly IemployeeBLL _employeeBLL;
        public EmployeeDashboardController(IemployeeBLL employeeBLL)
        {
            _employeeBLL = employeeBLL;
        }
        public IActionResult Index()
        {
            var empDtoJson = HttpContext.Session.GetString("employee");
            var empDtoList = JsonSerializer.Deserialize<List<employeeDTO>>(empDtoJson);
            if (empDtoList != null && empDtoList.Count > 0)
            {
                // Assuming you want to display the first user's information
                var firstempDto = empDtoList[0];
                ViewBag.Message = $"Welcome {firstempDto.First_Name} {firstempDto.Last_Name}";
                var Emp = _employeeBLL.GetEmployeesByID(firstempDto.Employee_ID).FirstOrDefault();
                var empOrderPlaced = _employeeBLL.GetEmployee_OrderPlacedDTOs(firstempDto.Employee_ID);
                ViewBag.OrderPlaced = empOrderPlaced;
                ViewBag.City = Emp?.EmployeeLocations.City;

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult AddLocation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddLocation(EmployeeLocationCreateDTO addresses)
        {
            try
            {
                var userDtoJson = HttpContext.Session.GetString("employee");
                var empDtoList = JsonSerializer.Deserialize<List<employeeDTO>>(userDtoJson);
                var firstUserDto = empDtoList?[0];
                var EmpID = firstUserDto.Employee_ID;

                // Set the customer ID for the address
                addresses.Employee_ID = EmpID;
                _employeeBLL.AddAddressEmp(addresses);

            }
            catch (Exception)
            {
                return View(addresses);
            }

            return RedirectToAction("Index");

        }
        public IActionResult AddSchedule()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSchedule(EmployeeCreateSchedule schedule)
        {
            try
            {
                var userDtoJson = HttpContext.Session.GetString("employee");
                var empDtoList = JsonSerializer.Deserialize<List<employeeDTO>>(userDtoJson);
                var firstUserDto = empDtoList?[0];
                var EmpID = firstUserDto.Employee_ID;

                // Set the customer ID for the address
                schedule.Employee_ID = EmpID;
                _employeeBLL.EmpSchedule(schedule);

            }
            catch (Exception)
            {
                return View(schedule);
            }

            return RedirectToAction("Index");

        }
        public IActionResult AddPickup()
        {
            var userDtoJson = HttpContext.Session.GetString("employee");
            var empDtoList = JsonSerializer.Deserialize<List<employeeDTO>>(userDtoJson);
            var firstUserDto = empDtoList?[0];
            var EmpID = firstUserDto.Employee_ID;
            var empOrderPlaced = _employeeBLL.GetEmployee_OrderPlacedDTOs(firstUserDto.Employee_ID);
            ViewBag.order = empOrderPlaced;
            return View();
        }
        [HttpPost]
        public IActionResult AddPickup(EmployeeInsertPickup pickup)
        {
            try
            {
                var userDtoJson = HttpContext.Session.GetString("employee");
                var empDtoList = JsonSerializer.Deserialize<List<employeeDTO>>(userDtoJson);
                var firstUserDto = empDtoList?[0];
                var EmpID = firstUserDto.Employee_ID;
                // Set the customer ID for the address
                pickup.Employee_ID = EmpID;
                _employeeBLL.AddPickup(pickup);

            }
            catch (Exception)
            {
                return View(pickup);
            }

            return RedirectToAction("Index");

        }

        public IActionResult PickupHistory()
        {
            try
            {
                var userDtoJson = HttpContext.Session.GetString("employee");
                var empDtoList = JsonSerializer.Deserialize<List<employeeDTO>>(userDtoJson);
                var firstUserDto = empDtoList?[0];

                var empOrderPickup = _employeeBLL.GetPickups(firstUserDto.Employee_ID);
                ViewBag.PickupList = empOrderPickup;
                return View();
            }
            catch (ArgumentException ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                return View();
            }


        }
    }
}
