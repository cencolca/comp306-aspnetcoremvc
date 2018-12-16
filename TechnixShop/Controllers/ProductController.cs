using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnixShop.Data;
using TechnixShop.Models;

namespace TechnixShop.Services
{
    public class ProductFinder
    {
        //private readonly ApplicationDbContext db;
        //public IConfiguration Configuration { get; }

        //internal Product find(string id)
        //{
        //    var opBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        //    opBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

        //    var product = db.Product
        //                    .Where(p => p.Id.Equals(id))
        //                    .FirstOrDefault();
        //    return product;
        //}

        //internal dynamic findAll()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
