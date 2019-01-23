using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hehe.Models.DBModels
{                       
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}