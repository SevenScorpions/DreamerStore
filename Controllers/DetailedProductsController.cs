using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamerStore2.Models;
using Microsoft.IdentityModel.Tokens;

namespace DreamerStore2.Controllers
{
    public class DetailedProductsController : Controller
    {
        private readonly SonungvienContext _context;

        public DetailedProductsController(SonungvienContext context)
        {
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

        // GET: DetailedProducts/Details/5

        // GET: DetailedProducts/Create
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
            return View();
        }

        // POST: DetailedProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetailedProductId,DetailedProductPrice,DetailedProductQuantity,DetailedProductName,ProductId,Order,Meta,Image,Hide,CreatedAt,UpdatedAt")] DetailedProduct detailedProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detailedProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", detailedProduct.ProductId);
            return View(detailedProduct);
        }

        // GET: DetailedProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetailedProducts == null)
            {
                return NotFound();
            }

            var detailedProduct = await _context.DetailedProducts.FindAsync(id);
            if (detailedProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", detailedProduct.ProductId);
            return View(detailedProduct);
        }

        // POST: DetailedProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetailedProductId,DetailedProductPrice,DetailedProductQuantity,DetailedProductName,ProductId,Order,Meta,Image,Hide,CreatedAt,UpdatedAt")] DetailedProduct detailedProduct)
        {
            if (id != detailedProduct.DetailedProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detailedProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailedProductExists(detailedProduct.DetailedProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", detailedProduct.ProductId);
            return View(detailedProduct);
        }

        // GET: DetailedProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetailedProducts == null)
            {
                return NotFound();
            }

            var detailedProduct = await _context.DetailedProducts
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.DetailedProductId == id);
            if (detailedProduct == null)
            {
                return NotFound();
            }

            return View(detailedProduct);
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
