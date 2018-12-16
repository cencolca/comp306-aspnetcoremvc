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
        private readonly ApplicationDbContext db;
        public ProductFinder()
        {

        }
        internal Product find(string id)
        {
            var product = db.Product
                            .Where(p => p.Id.Equals(id))
                            .FirstOrDefault();
            return product;
        }

        //internal dynamic findAll()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
