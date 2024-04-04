using DreamerStore2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;

public class CartController : Controller
{
    private readonly SonungvienContext _context;

    public CartController(SonungvienContext context)
    {
        _context = context;
    }

    // GET: Cart
    public IActionResult Index()
    {
        return View(GetCartFromCookie());
    }

    [HttpPost]
    public async Task<ActionResult> AddToCart(int detailedProductId, int quantity)
    {
        Cart cart = GetCartFromCookie();
        DetailedProduct? detailedProduct = await _context.DetailedProducts.FindAsync(detailedProductId);

        bool isAdded = false;
        if (detailedProduct != null)
        {
            isAdded = cart.AddToCart(detailedProduct, quantity);
            SaveCartToCookie(cart);
        }

        var responseObject = new
        {
            Success = isAdded
        };
        var json = JsonConvert.SerializeObject(responseObject);
        return Content(json, "application/json");
    }

    public ActionResult RemoveFromCart(int id, int quantity)
    {
        Cart cart = GetCartFromCookie();
        bool isDeleted = cart.RemoveFromCart(id, quantity);
        SaveCartToCookie(cart);
        var responseObject = new
        {
            Success = isDeleted
        };
        var json = JsonConvert.SerializeObject(responseObject);
        return Content(json, "application/json");
    }

    public ActionResult RemoveAll()
    {
        DeleteCartFromCookie(GetCartFromCookie());
        return RedirectToAction("Index");
    }

    private Cart GetCartFromCookie()
    {
        string jsonCart = Request.Cookies["CartData"];
        if (jsonCart != null)
        {
            return JsonConvert.DeserializeObject<Cart>(jsonCart);
        }
        return new Cart();
    }

    private void SaveCartToCookie(Cart cart)
    {
        string jsonCart = JsonConvert.SerializeObject(cart);
        CookieOptions option = new CookieOptions();
        option.Expires = DateTime.Now.AddMonths(1); // Expiration time for the cookie
        Response.Cookies.Append("CartData", jsonCart, option);
    }

    private void DeleteCartFromCookie(Cart cart)
    {
        cart.RemoveAll();
        Response.Cookies.Delete("CartData");
    }

}
