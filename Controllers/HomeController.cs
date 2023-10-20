using DreamerStore2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DreamerStore2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SonungvienContext _sonungvienContext;
        private readonly GoogleUploadingService _googleUploadingService;

        public HomeController(ILogger<HomeController> logger, SonungvienContext sonungvienContext, GoogleUploadingService googleUploadingService)
        {
            _logger = logger;
            _sonungvienContext = sonungvienContext;
            _googleUploadingService = googleUploadingService;
        }

        public IActionResult Index()
        {
            List<Category> categories = _sonungvienContext.Categories.ToList();
            foreach (var category in categories)
            {
                if(category.Hide==false)
                {
                    categories.Remove(category);
                }
            }
            return View(categories);
        }
        public IActionResult GetCategories()
        {
            var categories = _sonungvienContext.Categories.ToList(); // Lấy danh sách các danh mục từ cơ sở dữ liệu
            return Json(categories); // Trả về danh sách danh mục dưới dạng JSON
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}