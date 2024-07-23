using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace InventoryManagementSystem.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       // [Route("Category/Result")]
        public IActionResult NotFoundError()
        {
           // var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
              
            return View();
        }
    }
}
