using System;
using System.Collections.Generic;

namespace TechnixShop.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategory = new HashSet<ProductCategory>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
