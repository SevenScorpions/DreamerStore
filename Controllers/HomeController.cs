using DreamerStore2.Models;
using DreamerStore2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var categories = await _sonungvienContext.Categories
                .Where(c => c.Hide==true)
                .Take(2)
                .ToListAsync();

            var categoryViewModels = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                var categoryViewModel = new CategoryViewModel(category);
                categoryViewModel.Products = new List<ProductViewModel>();

                var products = await _sonungvienContext.Products
                    .Where(p => p.CategoryId == category.CategoryId && p.Hide == true)
                    .Take(8)
                    .ToListAsync();

                foreach (var product in products)
                {
                    var productViewModel = new ProductViewModel(product);
                    productViewModel.Image = _googleUploadingService.GetImage(product.Image);
                    categoryViewModel.Products.Add(productViewModel);
                }

                categoryViewModels.Add(categoryViewModel);
            }

            return View(categoryViewModels);
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
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}