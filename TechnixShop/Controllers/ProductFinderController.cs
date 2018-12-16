using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnixShop.Data;
using TechnixShop.Models;


namespace TechnixShop.Controllers
{
    public class ProductFinderController : Controller
    {
        //private ApplicationDbContext db;
        
        //public ProductFinderController(ApplicationDbContext context)
        //{
        //    db = context;
        //}

        internal Product find(string id)
        {
            //https://stackoverflow.com/questions/38417051/what-goes-into-dbcontextoptions-when-invoking-a-new-dbcontext
            DbContextOptionsBuilder<ApplicationDbContext> opBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            opBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-TechnixShop;Trusted_Connection=True;MultipleActiveResultSets=true");

            //DbContextOptions<ApplicationDbContext> op = new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>();
            
            
            using (ApplicationDbContext db = new ApplicationDbContext(opBuilder.Options))
            {
                //DbSet<Product> dbSet = db.Product;

                var product = db.Product
                             .Where(p => p.Id.ToString().Equals(id))
                             .FirstOrDefault();
                return product;
            }




               
        }
    }
}