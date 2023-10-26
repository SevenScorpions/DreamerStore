using DreamerStore2.Models;
using DreamerStore2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
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
            List<Category> categories = _sonungvienContext.Categories.ToList();
            List<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();
            int i = 0;
            foreach (var category in categories)
            {
                i++;
                if(i>2)
                {
                    break;
                }
                if(category.Hide==true)
                {
                    var categoryViewModel = new CategoryViewModel(category);
                    categoryViewModel.Products = new List<ProductViewModel>();
                    var products  = _sonungvienContext.Products.Where(p => p.CategoryId == category.CategoryId).ToList();
                    var j = 0;
                    foreach (var product in products)
                    {
                        j++;
                        if (j > 8)
                            break;
                        var productViewModel = new ProductViewModel(product);
                        productViewModel.Image = _googleUploadingService.GetImage(product.Image);
                        categoryViewModel.Products.Add(productViewModel);
                    }
                    Debug.WriteLine(categoryViewModel.Products.Count);
                    categoryViewModels.Add(categoryViewModel);
                }
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