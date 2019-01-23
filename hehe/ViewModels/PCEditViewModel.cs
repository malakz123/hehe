using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hehe.ViewModels
{
    public class PCEditViewModel
    {
        public int ProductCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}