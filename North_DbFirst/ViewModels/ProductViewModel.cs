using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace North_DbFirst.ViewModels
{
   public class ProductViewModel
    {
        public string Productname { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitStock { get; set; }
        public decimal? Maliyet { get; set; }
    }
}
