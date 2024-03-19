using Microsoft.AspNetCore.Mvc;
using OjoREGED.BLL.DTOs;
using OjoREGED.BLL.Interfaces;
using System.Text.Json;
namespace SampleMVC.Controllers;

public class HomeController : Controller
{
    private readonly ICustomerBLL _customerBLL;
    private readonly IemployeeBLL _employeeBLL;


    public HomeController(ICustomerBLL customerBLL, IemployeeBLL employeeBLL)
    {
        _customerBLL = customerBLL;
        _employeeBLL = employeeBLL;
    }
    // Home/Index
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult LoginEmployee()
    {
        return View();
    }
    [HttpPost]
    public IActionResult LoginEmployee(EmployeeLogin loginEmp)
    {
        try
        {
            var employeeLogin = _employeeBLL.EmployeeLogin(loginEmp);
            var empDTOsession = JsonSerializer.Serialize(employeeLogin);
            var login = employeeLogin.FirstOrDefault();
            if (login?.Role_ID == 1)
            {
                HttpContext.Session.SetString("admin", empDTOsession);
                return RedirectToAction("Index", "AdminDashboard");
            }
            else
            {
                HttpContext.Session.SetString("employee", empDTOsession);
                return RedirectToAction("Index", "EmployeeDashboard");

            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    public IActionResult RegisterEmployee()
    {
        return View();
    }
    [HttpPost]
    public IActionResult RegisterEmployee(CreateEmployeeDTO createEmployee)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        try
        {
            _employeeBLL.Insertemployee(createEmployee);
            ViewBag.Message = @"<div class='alert alert-success'><strong>Success!&nbsp;</strong>Registration process successfully !</div>";

        }
        catch (Exception ex)
        {
            ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
        }

        return RedirectToAction("LoginEmployee");
    }
    public IActionResult LoginCustomer()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LoginCustomer(CustomerLogin customerDTO)
    {
        try
        {
            var UserLogin = _customerBLL.CustomerLogin(customerDTO);
            var userDTOsession = JsonSerializer.Serialize(UserLogin);
            HttpContext.Session.SetString("user", userDTOsession);
            var login = UserLogin.FirstOrDefault();
            if (login?.Role_ID == 1)
            {
                return RedirectToAction("Index", "AdminDashboard");
            }


            return RedirectToAction("Index", "CustomerDashboard");
        }
        catch (Exception)
        {

            throw;

        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("user");
        HttpContext.Session.Remove("employee");
        HttpContext.Session.Remove("admin");
        return RedirectToAction("Index");
    }

    public IActionResult RegisterCustomer()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegisterCustomer(CustomerCreateDTO Customer)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        try
        {
            _customerBLL.InsertCustomer(Customer);
            ViewBag.Message = @"<div class='alert alert-success'><strong>Success!&nbsp;</strong>Registration process successfully !</div>";

        }
        catch (Exception ex)
        {
            ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
        }

        return RedirectToAction("LoginCustomer");
    }


}
