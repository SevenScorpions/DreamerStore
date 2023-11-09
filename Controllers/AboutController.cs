using Microsoft.AspNetCore.Mvc;

namespace DreamerStore2.Controllers
{
    public class AboutController : Controller
    {
        // GET: AboutController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AboutController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: AboutController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AboutController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AboutController/Edit/5
    }
}
