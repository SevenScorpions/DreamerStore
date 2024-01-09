using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamerStore2.Models;
using Microsoft.IdentityModel.Tokens;
using DreamerStore2.Service.ImageUploading;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;


namespace DreamerStore2.Controllers
{
    [Authorize]
    public class DetailedProductsController : Controller
    {
        private readonly ImageUploadingService _imageUploadingService;
        private readonly SonungvienContext _context;

        public DetailedProductsController(SonungvienContext context, ImageUploadingService imageUploadingService)
        {
            _imageUploadingService = imageUploadingService;
            _context = context;
        }

        // GET: DetailedProducts
        public async Task<IActionResult> Index(string? id)
        {
            var sonungvienContext = new List<DetailedProduct>();
            if (id.IsNullOrEmpty())
            {
                sonungvienContext = await _context.DetailedProducts.Include(d => d.Product).ToListAsync();
            }
            else
            {
                var product = await _context.Products.Where(p => p.Meta == id).FirstOrDefaultAsync();
                sonungvienContext = await _context.DetailedProducts.Include(d => d.Product).Where(d => d.ProductId == product.ProductId).ToListAsync();
            }
            ViewBag.id = id;
            return View(sonungvienContext);
        }

        public IActionResult Create(string? id)
        {
            if (id.IsNullOrEmpty())
            {
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            }
            else
            {
                ViewData["ProductId"] = new SelectList(_context.Products.Where(p=>p.Meta==id), "ProductId", "ProductName");
            }
            ViewBag.id = id;
            return View();
        }

        // POST: DetailedProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetailedProductId,DetailedProductPrice,DetailedProductQuantity,DetailedProductName,ProductId,Order,Meta,Image,Hide,CreatedAt,UpdatedAt")] DetailedProduct detailedProduct,IFormFile photo, string? id)
        {
            
            if (!detailedProduct.DetailedProductName.IsNullOrEmpty())
            {
                if(!detailedProduct.Order.HasValue)
                {
                    detailedProduct.Order = 0;
                }
                if(detailedProduct.Meta.IsNullOrEmpty())
                {
                    detailedProduct.Meta = Guid.NewGuid().ToString();
                }
                if(detailedProduct.DetailedProductQuantity<0)
                {
                    detailedProduct.DetailedProductQuantity = 0;
                }
                if(detailedProduct.DetailedProductPrice<0)
                {
                    detailedProduct.DetailedProductPrice = 0;
                }
                detailedProduct.CreatedAt = DateTime.Now;
                detailedProduct.UpdatedAt = DateTime.Now;
                if(photo!=null)
                {
                    detailedProduct.Image = await _imageUploadingService.AddImageAsync(photo);
                }
                _context.Add(detailedProduct);
                await _context.SaveChangesAsync();
                if(id.IsNullOrEmpty())
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Index", new { id = id});
                }
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", detailedProduct.ProductId);
            return View(detailedProduct);
        }

        // GET: DetailedProducts/Edit/5
        public async Task<IActionResult> Edit(string? id, string? pid)
        {
            if (id.IsNullOrEmpty() || _context.DetailedProducts == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var detailedProduct = await _context.DetailedProducts.Where(dp=>dp.Meta==id).FirstOrDefaultAsync();
            if (detailedProduct == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.id = pid;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", detailedProduct.ProductId);
            return View(detailedProduct);
        }

        // POST: DetailedProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DetailedProductId,DetailedProductPrice,DetailedProductQuantity,DetailedProductName,ProductId,Order,Meta,Image,Hide,CreatedAt,UpdatedAt")] DetailedProduct detailedProduct,IFormFile? photo, string? pid)
        {
            var existingDP = await _context.DetailedProducts.FirstOrDefaultAsync(p => p.Meta == id);
            if (!detailedProduct.DetailedProductName.IsNullOrEmpty())
            {
                try
                {
                    existingDP.DetailedProductName = detailedProduct.DetailedProductName;
                    existingDP.Meta = detailedProduct.Meta;
                    existingDP.DetailedProductPrice = detailedProduct.DetailedProductPrice;
                    existingDP.DetailedProductQuantity = detailedProduct.DetailedProductQuantity;
                    existingDP.Hide = detailedProduct.Hide;
                    existingDP.Order = detailedProduct.Order;
                    existingDP.UpdatedAt = DateTime.Now;
                    existingDP.ProductId = detailedProduct.ProductId;
                    if (!existingDP.Order.HasValue)
                    {
                        existingDP.Order = 0;
                    }
                    if (existingDP.Meta.IsNullOrEmpty())
                    {
                        existingDP.Meta = Guid.NewGuid().ToString();
                    }
                    if (existingDP.DetailedProductQuantity < 0)
                    {
                        existingDP.DetailedProductQuantity = 0;
                    }
                    if (existingDP.DetailedProductPrice < 0)
                    {
                        existingDP.DetailedProductPrice = 0;
                    }
                    if (photo != null)
                    {
                        if(!existingDP.Image.IsNullOrEmpty())
                        { 
                            await _imageUploadingService.DeleteImageAsync(existingDP.Image);
                        }
                        existingDP.Image = await _imageUploadingService.AddImageAsync(photo);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailedProductExists(detailedProduct.DetailedProductId))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                if(id.IsNullOrEmpty())
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Index", new { id = pid });
                }
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", detailedProduct.ProductId);
            return View(detailedProduct);
        }

        // GET: DetailedProducts/Delete/5
        public async Task<IActionResult> Delete(string? id, string? pid)
        {
            if (_context.DetailedProducts == null)
            {
                return Problem("Entity set 'SonungvienContext.DetailedProducts'  is null.");
            }
            var detailedProduct = await _context.DetailedProducts.Where(dp=>dp.Meta==id).FirstOrDefaultAsync();
            if (detailedProduct != null)
            {
                if(!detailedProduct.Image.IsNullOrEmpty())
                {
                    await _imageUploadingService.DeleteImageAsync(detailedProduct.Image);
                }
                _context.DetailedProducts.Remove(detailedProduct);
            }

            await _context.SaveChangesAsync();
            if (id.IsNullOrEmpty())
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", new { id = pid });
            }
        }

        // POST: DetailedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetailedProducts == null)
            {
                return Problem("Entity set 'SonungvienContext.DetailedProducts'  is null.");
            }
            var detailedProduct = await _context.DetailedProducts.FindAsync(id);
            if (detailedProduct != null)
            {
                _context.DetailedProducts.Remove(detailedProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailedProductExists(int id)
        {
          return (_context.DetailedProducts?.Any(e => e.DetailedProductId == id)).GetValueOrDefault();
        }
    }
}
