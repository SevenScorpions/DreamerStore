using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamerStore2.Models;
using System.Diagnostics;
using DreamerStore2.ViewModel;
using Microsoft.IdentityModel.Tokens;

namespace DreamerStore2.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly SonungvienContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly GoogleUploadingService _uploadingService;

        public CategoriesController(SonungvienContext context,IWebHostEnvironment environment, GoogleUploadingService googleUploadingService)
        {
            _context = context;
            _webHostEnvironment = environment;
            _uploadingService = googleUploadingService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :RedirectToAction("Error", "Home");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return RedirectToAction("Error", "Home");
            }
            CategoryViewModel model = new CategoryViewModel(category);

            return View(model);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Order,Meta,Image,Hide,CreatedAt,UpdatedAt")] Category category,IFormFile photo)
        {
            if(!category.Order.HasValue)
            {
                category.Order = 1;
            }
            if(photo!=null)
            {
                category.Image = await _uploadingService.UploadImage(photo);
            }
            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;
            if (!category.CategoryName.IsNullOrEmpty())
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
/*            if (!ModelState.IsValid)
            {
                foreach (var modelStateValue in ModelState.Values)
                {
                    if (modelStateValue.Errors.Count > 0)
                    {
                        // Xử lý lỗi cho modelStateValue
                        // Ví dụ: In ra tên thuộc tính và thông báo lỗi đầu tiên
                        var propertyName = modelStateValue.AttemptedValue;
                        var errorMessage = modelStateValue.Errors[0].ErrorMessage;
                        Debug.WriteLine($"Property '{propertyName}' has error: {errorMessage}");
                    }
                }
            }*/
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Order,Meta,Image,Hide,CreatedAt,UpdatedAt")] Category category, IFormFile photo)
        {
            if (id != category.CategoryId)
            {
                return RedirectToAction("Error", "Home");
            }
            Category existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return RedirectToAction("Error", "Home");
            }
            if (!category.CategoryName.IsNullOrEmpty())
            {
                try
                {
                    if (photo != null)
                    {
                        if (!string.IsNullOrEmpty(existingCategory.Image))
                        {
                            _uploadingService.DeleteImage(existingCategory.Image);
                        }
                        category.Image = await _uploadingService.UploadImage(photo);
                    }

                    existingCategory.CategoryName = category.CategoryName;
                    existingCategory.Order = category.Order;
                    existingCategory.Meta = category.Meta;
                    existingCategory.Image = category.Image;
                    existingCategory.Hide = category.Hide;
                    existingCategory.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction("Error","Home");
                }
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'SonungvienContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                if(!category.Image.IsNullOrEmpty())
                    _uploadingService.DeleteImage(category.Image);
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'SonungvienContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
