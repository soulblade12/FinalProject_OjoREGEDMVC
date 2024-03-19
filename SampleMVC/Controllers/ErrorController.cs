using Microsoft.AspNetCore.Mvc;

namespace SampleMVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/Error404")]
        public IActionResult Error404()
        {
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("employee");
            HttpContext.Session.Remove("admin");
            return View();
        }
    }
}
