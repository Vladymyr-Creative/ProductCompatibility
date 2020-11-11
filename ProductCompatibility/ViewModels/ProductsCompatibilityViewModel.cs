using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.ViewModels
{
    public class ProductsCompatibilityViewModel
    {
        public IEnumerable<ProductsCompatibility> AllProdCompatibilities { get; set; }
        public IEnumerable<Product> AllProducts { get; set; }
        public IEnumerable<Compatibility> Compatibilities { get; set; }
    }
}
