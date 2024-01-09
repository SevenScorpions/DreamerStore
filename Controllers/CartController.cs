using DreamerStore2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DreamerStore2.Controllers
{
    public class CartController : Controller
    {
        private readonly SonungvienContext _context;
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("Cart");
            Cart Cart = JsonConvert.DeserializeObject<Cart>(cart);
            if(Cart == null)
            {
                return View();
            }
            else
            {
                return View("Index", Cart);
            }
        }
        public bool AddDetailedProductToCart(int id)
        {
            bool isAdded = false;
            string cart = HttpContext.Session.GetString("Cart");
            Cart cartObj;
            if (cart == null)
            {
                cartObj = new Cart();
            }
            else
            {
                cartObj = JsonConvert.DeserializeObject<Cart>(cart);
            }
            DetailedProduct product = _context.DetailedProducts.FirstOrDefault(p => p.DetailedProductId == id);
            if (!cartObj.ProductList.ContainsKey(product.ProductId))
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
            string jsonCart = JsonConvert.SerializeObject(cartObj);
            HttpContext.Session.SetString("Cart", jsonCart);
            return isAdded;
        }
        public async Task<IActionResult> RemoveDetailedProduct(int id)
        {
            string cart = HttpContext.Session.GetString("Cart");
            Cart cartObj = JsonConvert.DeserializeObject<Cart>(cart);
            if (cartObj != null)
            {
                DetailedProduct product = _context.DetailedProducts.FirstOrDefault(p => p.DetailedProductId == id);
                if (cartObj.ProductList.ContainsKey(product.ProductId))
                {
                    OrderedDetailedProduct orderedDetailedProduct = cartObj.ProductList[product.ProductId];
                    cartObj.CartTotalPrice -= orderedDetailedProduct.DetailedProduct.DetailedProductPrice * orderedDetailedProduct.Quantity;
                    cartObj.ProductList.Remove(product.ProductId);
                }
                string jsonCart = JsonConvert.SerializeObject(cartObj);
                HttpContext.Session.SetString("Cart", jsonCart);
            }
            return RedirectToAction("Index");
        }
        public void RemoveCart()
        {
            HttpContext.Session.Remove("Cart");
        }
    }
}
