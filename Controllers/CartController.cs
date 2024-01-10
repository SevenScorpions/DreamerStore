using DreamerStore2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DreamerStore2.Controllers
{
    public class CartController : Controller
    {
        private readonly SonungvienContext _context;

        public CartController(SonungvienContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cartKey = HttpContext.Session.GetString("Cart_" + HttpContext.Session.Id);
            if (cartKey == null)
            {
                return View();
            }
            else
            {
                Cart Cart = JsonConvert.DeserializeObject<Cart>(cartKey);
                return View("Index", Cart);
            }
        }
        public bool AddToCart(int id)
        {
            bool isAdded = false;
            var cartKey = HttpContext.Session.GetString("Cart_" + HttpContext.Session.Id);
            Cart cartObj;
            Console.WriteLine(cartKey);
            if (cartKey == null)
            {
                cartObj = new Cart();
            }
            else
            {
                cartObj = JsonConvert.DeserializeObject<Cart>(cartKey);
            }
            DetailedProduct product = _context.DetailedProducts.Where(p => p.DetailedProductId == id).FirstOrDefault();
            if (!cartObj.ProductList.ContainsKey(product.DetailedProductId))
            {
                if (product.DetailedProductQuantity > 0)
                {
                    cartObj.ProductList.Add(product.ProductId, new OrderedDetailedProduct(product, 1));
                    cartObj.CartTotalPrice += cartObj.ProductList[product.ProductId].DetailedProduct.DetailedProductPrice;
                    isAdded = true;
                }
            }
            else
            {
                if (product.DetailedProductQuantity > cartObj.ProductList[product.ProductId].Quantity)
                {
                    OrderedDetailedProduct orderedDetailedProduct = cartObj.ProductList[product.ProductId];
                    cartObj.ProductList[product.ProductId].Quantity += 1;
                    cartObj.CartTotalPrice += orderedDetailedProduct.DetailedProduct.DetailedProductPrice;
                    isAdded = true;
                }
            }
            cartKey = JsonConvert.SerializeObject(cartObj);
            HttpContext.Session.SetString("Cart", cartKey);
            return isAdded;
        }
        public async Task<IActionResult> RemoveDetailedProduct(int id)
        {
            var cartKey = HttpContext.Session.GetString("Cart_" + HttpContext.Session.Id);
            Cart cartObj = JsonConvert.DeserializeObject<Cart>(cartKey);
            if (cartObj != null)
            {
                DetailedProduct product = _context.DetailedProducts.FirstOrDefault(p => p.DetailedProductId == id);
                if (cartObj.ProductList.ContainsKey(product.ProductId))
                {
                    OrderedDetailedProduct orderedDetailedProduct = cartObj.ProductList[product.ProductId];
                    cartObj.CartTotalPrice -= orderedDetailedProduct.DetailedProduct.DetailedProductPrice * orderedDetailedProduct.Quantity;
                    cartObj.ProductList.Remove(product.ProductId);
                }
                cartKey = JsonConvert.SerializeObject(cartObj);
                HttpContext.Session.SetString("Cart_" + HttpContext.Session.Id, cartKey);
            }
            return RedirectToAction("Index");
        }
        public void RemoveCart()
        {
            HttpContext.Session.Remove("Cart_" + HttpContext.Session.Id);
        }
    }
}
