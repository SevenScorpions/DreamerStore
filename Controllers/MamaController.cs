using DreamerStore2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var account = await _context.Accounts.Where(a => a.Username == username).FirstOrDefaultAsync();
            ViewBag.State = "none";
            if (account == null)
            {
                ViewBag.State = "failed";
                return View();
            }
            else if (!ComparePasswords(password, account.Password))
            {
                ViewBag.State = "failed";
                return View();
            }
            ViewBag.State = "failed";
            HttpContext.Session.SetString("Login State","success");
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
    }
}
