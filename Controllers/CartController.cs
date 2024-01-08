using Microsoft.AspNetCore.Mvc;

namespace DreamerStore2.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
