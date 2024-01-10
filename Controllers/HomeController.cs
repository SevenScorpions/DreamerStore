using DreamerStore2.Models;
using DreamerStore2.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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


            foreach (var category in categories)
            {

                var products = await _sonungvienContext.Products
                    .Where(p => p.CategoryId == category.CategoryId && p.Hide == true)
                    .Take(8)
                    .ToListAsync();
                category.Products = products;
            }

            return View(categories);
        }
        public async Task<IActionResult> ListProduct(string? c, int page)
        {
            var products = new List<Product>();
            Category category = await _sonungvienContext.Categories.FirstOrDefaultAsync(m => m.Meta == c);
            if((category==null && !c.IsNullOrEmpty())|| page<0)
            {
                return RedirectToAction("Error");
            }
            else if(category==null)
            {
                products = await _sonungvienContext.Products
                    .Where(p => p.Hide == true)
                    .Take(20)
                    .ToListAsync();
            }
            else
            {
                products = await _sonungvienContext.Products
                    .Where(p => p.CategoryId == category.CategoryId && p.Hide == true)
                    .Take(20)
                    .ToListAsync();
            }
            return View(products);
        }
        public async Task<IActionResult> ListDetailedProduct(int id)
        {
            Product product = await _sonungvienContext.Products.FirstOrDefaultAsync(m => m.ProductId == id && m.Hide == true);
            if(product != null)
            {
                var detailedProducts = await _sonungvienContext.DetailedProducts
                        .Where(p => p.ProductId == product.ProductId && p.Hide == true)
                        .Take(20)
                        .ToListAsync();
                DetailedProductViewModel detailedProductViewModel = new DetailedProductViewModel(detailedProducts, product);
                return View(detailedProductViewModel);
            }
            return View("Error");
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