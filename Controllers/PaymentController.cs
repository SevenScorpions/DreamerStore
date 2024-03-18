using Microsoft.AspNetCore.Mvc;
using DreamerStore2.Api;
using DreamerStore2.Models;
namespace DreamerStore2.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApiClient _apiClient;
        public PaymentController()
        {
            _apiClient = new ApiClient();
        }

        //Apply web api
        public async Task<IActionResult> Index()
        {
            IEnumerable<WeatherForecast> weatherForecasts = await _apiClient.GetAsync<IEnumerable<WeatherForecast>>("WeatherForecast");
            if (weatherForecasts != null)
            {
                return View("Index", weatherForecasts);
            }
            return RedirectToAction("Index", "Home");
        }

       
    }
}
