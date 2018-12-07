using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnixShop.Models.ProductViewModels
{
    public class AddProductModel
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public IFormFile Image { get; set; }

        private List<int> _selectedCategories;
        public List<int> SelectedCategories
        {
            get
            {
                if (Product == null) return null;

                if (_selectedCategories == null)
                {
                    _selectedCategories = Product.ProductCategory.Select(c => c.CategoryId).ToList();
                }
                return _selectedCategories;
            }
            set { _selectedCategories = value; }
        }
    }
}
