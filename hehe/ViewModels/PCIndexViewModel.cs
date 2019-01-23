using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace hehe.ViewModels
{
    public class PCIndexViewModel
    {
        public PCIndexViewModel()
        {
            PCList = new List<PCListViewModel>();
        }
        public class PCListViewModel
        {
            public int ProductCategoryId { get; set; }
            public string Name { get; set; }
        }
        public List<PCListViewModel> PCList { get; set; }
    }
}