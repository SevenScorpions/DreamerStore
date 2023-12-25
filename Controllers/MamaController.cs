using DreamerStore2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DreamerStore2.Controllers
{
    public class MamaController : Controller
    {
        private readonly SonungvienContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public MamaController(SonungvienContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }    
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            var account = await _context.Accounts.Where(a => a.Username == username).FirstOrDefaultAsync();
            ViewBag.State = "failed";
            if (account == null)
            {
                return View();
            }
            else if (!ComparePasswords(password, account.Password))
            {
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                // Các claims khác nếu cần
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuthenticationScheme");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("MyCookieAuthenticationScheme", principal);

            ViewBag.State = "sucessed";
            _session.SetString("UserId", username);
            if (!string.IsNullOrEmpty(returnUrl))
                return LocalRedirect(returnUrl);
            else
                return RedirectToAction("Index");
        }
        public bool ComparePasswords(string password, byte[] storedPasswordHash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return CompareByteArrays(passwordHash, storedPasswordHash);
            }
        }

        public bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            return RedirectToAction("Login");
        }
    }
}
