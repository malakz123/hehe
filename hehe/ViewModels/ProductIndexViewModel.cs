using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hehe.ViewModels
{
    public class ProductIndexViewModel
    {

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Search { get; set; }
        public string SortOrderName { get; set; }
        public string SortOrderPrice { get; set; }

        public ProductIndexViewModel()
        {
            ProductList = new List<ProductListViewModel>();
        }

        public class ProductListViewModel
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }

            public int ProductCategoryId { get; set; }
        }
        public List<ProductListViewModel> ProductList { get; set; }
    }
}