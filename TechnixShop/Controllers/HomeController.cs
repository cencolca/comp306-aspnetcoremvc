using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnixShop.Models;
using TechnixShop.Data;
using Microsoft.EntityFrameworkCore;

namespace TechnixShop.Controllers
{
    public class HomeController : Controller
    {
        private int pageSize = 20;

        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            bool hasFilter = false;
            // query filer arguments
            string query = Request.Query["q"].ToString().Trim();
            int catId = 0;
            try
            {
                catId = Int32.Parse(Request.Query["c"].ToString());
                
            }
            catch
            {
            }

            int page = 1;
            try
            {
                page = Int32.Parse(Request.Query["p"].ToString());

            }
            catch
            {
            }

            // do filtering
            var products = from p in db.Product select p;
            if (!query.Equals(""))
            {
                products = products.Where(p => p.Title.Contains(query));
                hasFilter = true;
            }
            if (catId > 0)
            {
                products = products.Where(p => p.ProductCategory.Any(c => c.CategoryId == catId));
                hasFilter = true;
            }

            // pagination
            int TotalItem = products.Count();
            products = products.Skip((page - 1) * pageSize).Take(pageSize);

            List<Category> categories = db.Category.ToList();
            ViewBag.categories = categories;
            ViewBag.NumberOfPages = Math.Ceiling((double) TotalItem / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.hasFilter = hasFilter;

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = db.Product
                            .Include(c => c.ProductCategory)
                            .ThenInclude(m => m.Category)
                            .Where(p => p.Id.Equals(id))
                            .First();

            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
