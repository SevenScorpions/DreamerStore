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
using DreamerStore2.Service.ImageUploading;

namespace DreamerStore2.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly SonungvienContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly GoogleUploadingService _uploadingService;
        private readonly ImageUploadingService _imageUploadingService;

        public CategoriesController(SonungvienContext context,IWebHostEnvironment environment, GoogleUploadingService googleUploadingService, ImageUploadingService imageUploadingService)
        {
            _context = context;
            _webHostEnvironment = environment;
            _uploadingService = googleUploadingService;
            _imageUploadingService = imageUploadingService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :RedirectToAction("Error", "Home");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id.IsNullOrEmpty() || _context.Categories == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Meta == id);
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
                category.Image = await _imageUploadingService.AddImageAsync(photo);
            }
            if(category.Meta.IsNullOrEmpty())
            {
                category.Meta = Guid.NewGuid().ToString();
            }
            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;
            if (!category.CategoryName.IsNullOrEmpty())
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id.IsNullOrEmpty() || _context.Categories == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c=>c.Meta==id);
            if (category == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CategoryId,CategoryName,Order,Meta,Image,Hide,CreatedAt,UpdatedAt")] Category category, IFormFile photo)
        {
            if (id != category.Meta)
            {
                return RedirectToAction("Error", "Home");
            }
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Meta == id);
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
                            await _imageUploadingService.DeleteImageAsync(existingCategory.Image);
                        }
                        category.Image = await _imageUploadingService.AddImageAsync(photo);
                    }
                    if (existingCategory.Meta.IsNullOrEmpty())
                    {
                        existingCategory.Meta = Guid.NewGuid().ToString();
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
        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                if (_context.Categories == null)
                {
                    return Problem("Entity set 'SonungvienContext.Categories'  is null.");
                }
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Meta == id);
                if (category != null)
                {
                    if(!category.Image.IsNullOrEmpty())
                        await _imageUploadingService.DeleteImageAsync(category.Image);
                    _context.Categories.Remove(category);
                }
                await _context.SaveChangesAsync();
            }
            catch { 
                RedirectToAction("Error", "Home");
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Categories/Delete/5

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
