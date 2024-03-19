using Microsoft.AspNetCore.Mvc;
using OjoREGED.BLL.DTOs;
using OjoREGED.BLL.Interfaces;

namespace SampleMVC.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly IemployeeBLL _employeeBLL;
        public AdminDashboardController(IemployeeBLL employeeBLL)
        {
            _employeeBLL = employeeBLL;
        }
        public IActionResult Index()
        {
            var adminSession = HttpContext.Session.GetString("admin");
            if (string.IsNullOrEmpty(adminSession))
            {

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Employee = _employeeBLL.GetAllEmployee();
            return View();
        }

        public IActionResult Edit(int id)
        {
            var model = _employeeBLL.GetEmployeesByID(id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, employeeDTO emp)
        {
            try
            {

                // Update the employee
                _employeeBLL.Update(emp);

            }
            catch (Exception ex)
            {
                ViewData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
                return View(emp);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            // _ArticleBLL.QueryString
            _employeeBLL.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
