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
            string cartKey = HttpContext.Session.GetString("Cart_" + HttpContext.Session.Id);
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

        [HttpPost]
        public async Task<ActionResult> AddToCart(int detailedProductId, int quantity)
        {
            DetailedProduct detailedProduct = await _context.DetailedProducts.FindAsync(detailedProductId);

            Cart cart;
            if (detailedProduct != null)
            {
                string cartKey = HttpContext.Session.GetString("Cart_" + HttpContext.Session.Id);
                if (cartKey == null)
                {
                    cart = new Cart();
                }
                else
                {
                    //lấy ra cart
                    cart = JsonConvert.DeserializeObject<Cart>(cartKey);
                }
                bool isAdded = cart.AddToCart(detailedProduct, cart, quantity);
                if(isAdded)
                {
                    cartKey = JsonConvert.SerializeObject(cart);
                    HttpContext.Session.SetString("Cart_" + HttpContext.Session.Id, cartKey);
                    //return Json(true); // Trả về true nếu thêm thành công
                }
            }
            return RedirectToAction("Index");
            //return Json(false); // Trả về false nếu sản phẩm không tồn tại
        }

        public async Task<ActionResult> RemoveItem(int id)
        {
            DetailedProduct detailedProduct = await _context.DetailedProducts.FindAsync(id);

            Cart cart;
            if (detailedProduct != null)
            {
                string cartKey = HttpContext.Session.GetString("Cart_" + HttpContext.Session.Id);
                cart = JsonConvert.DeserializeObject<Cart>(cartKey);
                if(cart.RemoveItemInCart(detailedProduct, cart))
                {
                    cartKey = JsonConvert.SerializeObject(cart);
                    HttpContext.Session.SetString("Cart_" + HttpContext.Session.Id, cartKey);
                    if(cart.isEmpty(cart))
                    {
                        cart.RemoveCart(HttpContext);
                    }
                    //return Json(true); // Trả về true nếu thêm thành công
                }
            }
            return RedirectToAction("Index");
            //return Json(false); // Trả về false nếu sản phẩm không tồn tại
        }

        public async Task<IActionResult> RemoveCart()
        {
            string cartKey = HttpContext.Session.GetString("Cart_" + HttpContext.Session.Id);
            Cart cart = JsonConvert.DeserializeObject<Cart>(cartKey);
            cart.RemoveCart(HttpContext);
            return RedirectToAction("Index");
        }
    }
}
