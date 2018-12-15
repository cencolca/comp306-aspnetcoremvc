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

        [Required, StringLength(50)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
