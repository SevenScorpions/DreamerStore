using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamerStore2.Models;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace DreamerStore2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SonungvienContext _context;
        private readonly GoogleUploadingService googleUploadingService;
        public ProductsController(SonungvienContext context, GoogleUploadingService googleUploadingService)
        {
            _context = context;
            this.googleUploadingService = googleUploadingService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var sonungvienContext = _context.Products.Include(p => p.Category);
            return View(await sonungvienContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDescription,Order,Meta,Hide,CategoryId")] Product product, List<IFormFile> photos)
        {
            product.Category = await _context.Categories.FindAsync(product.CategoryId);
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            product.ProductSold = 0;
            if (!product.ProductDescription.IsNullOrEmpty() && product.Category!=null)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                if (!photos.IsNullOrEmpty())
                {
                    foreach (var i in photos)
                    {
                        _context.Add(new ProductImage()
                        {
                            ProductId = product.ProductId,
                            ProductImageLink = await googleUploadingService.UploadImage(i)
                        });
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductDescription,ProductSold,Order,Meta,Hide,CreatedAt,UpdatedAt,CategoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return RedirectToAction("Error", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'SonungvienContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
