using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Models
{
    public class Compatibility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductsCompatibility> ProductsCompatibility { get; set; }
    }
}
