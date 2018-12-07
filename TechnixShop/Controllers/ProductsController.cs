using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnixShop.Data;
using TechnixShop.Models;
using TechnixShop.Models.ProductViewModels;

namespace TechnixShop.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            AddProductModel model = new AddProductModel();
            List<Category> categories = _context.Category.ToList();
            model.Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            });

            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddProductModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (int catId in model.SelectedCategories)
                {
                    model.Product.ProductCategory.Add(new ProductCategory { CategoryId = catId, Product = model.Product });
                }

                _context.Add(model.Product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Product
               .Include(p => p.ProductCategory)
               .Where(p => p.Id == id)
               .SingleAsync();
            if (product == null)
            {
                return NotFound();
            }

            AddProductModel model = new AddProductModel();

            List<Category> categories = _context.Category.ToList();
            model.Categories = categories.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            });
            model.Product = product;

            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddProductModel model, IFormCollection collection)
        {
            if (id != model.Product.Id)
            {
                return NotFound();
            }

            Product productToUpdate = _context.Product
               .Include(p => p.ProductCategory)
               .Where(p => p.Id == id)
               .Single();

            if (ModelState.IsValid)
            {
                try
                {
                    productToUpdate.ProductCategory.Clear();
                    foreach (int catId in model.SelectedCategories)
                    {
                        productToUpdate.ProductCategory.Add(new ProductCategory { CategoryId = catId, ProductId = id });
                    }

                    _context.Update(productToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
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
            return View(model);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
