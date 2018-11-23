using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechnixShop.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductCategory = new HashSet<ProductCategory>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int Rating { get; set; }

        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
