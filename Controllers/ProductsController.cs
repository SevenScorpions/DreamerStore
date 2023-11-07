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
using Microsoft.CodeAnalysis;
using DreamerStore2.Service.ImageUploading;

namespace DreamerStore2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SonungvienContext _context;
        private readonly GoogleUploadingService _googleUploadingService;
        private readonly ImageUploadingService _imageUploadingService;
        public ProductsController(SonungvienContext context, GoogleUploadingService googleUploadingService, ImageUploadingService imageUploadingService)
        {
            _context = context;
            _googleUploadingService = googleUploadingService;
            _imageUploadingService = imageUploadingService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var sonungvienContext = _context.Products.Include(p => p.Category);
            return View(await sonungvienContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id.IsNullOrEmpty() || _context.Products == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Meta == id);
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
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductDescription,Order,Meta,Hide,CategoryId")] Product product, List<IFormFile> photos)
        {
            var category = await _context.Categories.FindAsync(product.CategoryId);
            product.Category = category;
            if (product.Category != null)
            {
                if(product.Meta.IsNullOrEmpty())
                {
                    product.Meta = Guid.NewGuid().ToString();
                }
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                product.ProductSold = 0;
                _context.Add(product);
                await _context.SaveChangesAsync();
                if (!photos.IsNullOrEmpty())
                {
                    foreach (var i in photos)
                    {
                        var productImage = new ProductImage()
                        {
                            ProductId = product.ProductId,
                            ProductImageLink = await _imageUploadingService.AddImageAsync(i)
                        };
                        if (product.Image.IsNullOrEmpty())
                        {
                            product.Image = productImage.ProductImageLink;
                        }
                        _context.Add(productImage);
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id.IsNullOrEmpty() || _context.Products == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Meta == id);
            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductId,ProductName,ProductPrice,ProductDescription,ProductSold,Order,Meta,Hide,CreatedAt,UpdatedAt,CategoryId")] Product product,List<IFormFile> photos)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Meta == id);
            if (!product.ProductName.IsNullOrEmpty())
            {
                try
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.ProductDescription = product.ProductDescription;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.Meta = product.Meta;
                    existingProduct.Order = product.Order;
                    existingProduct.Hide = product.Hide;
                    existingProduct.ProductPrice = product.ProductPrice;
                    existingProduct.UpdatedAt = DateTime.Now;
                    if (existingProduct.Meta.IsNullOrEmpty())
                    {
                        existingProduct.Meta = Guid.NewGuid().ToString();
                    }
                    if (!photos.IsNullOrEmpty())
                    {
                        var productImages = await _context.ProductImages
                                                            .Where(p => p.ProductId == product.ProductId)
                                                            .ToListAsync();
                        foreach (var image in productImages)
                        {
                            await _imageUploadingService.DeleteImageAsync(image.ProductImageLink);
                            _context.Remove(image);
                        }
                        foreach (var i in photos)
                        {
                            var productImage = new ProductImage()
                            {
                                ProductId = product.ProductId,
                                ProductImageLink = await _imageUploadingService.AddImageAsync(i)
                            };
                            if (product.Image.IsNullOrEmpty())
                            {
                                product.Image = productImage.ProductImageLink;
                            }
                            _context.Add(productImage);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }
        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                if (_context.Products == null)
                {
                    return Problem("Entity set 'SonungvienContext.Products'  is null.");
                }
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Meta == id);
                var productImages = await _context.ProductImages
                                                            .Where(p => p.ProductId == product.ProductId)
                                                            .ToListAsync();
                foreach (var image in productImages)
                {
                    await _imageUploadingService.DeleteImageAsync(image.ProductImageLink);
                    _context.Remove(image);
                }
                if (product != null)
                {
                    _context.Products.Remove(product);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction(nameof(Index));
        }
        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
