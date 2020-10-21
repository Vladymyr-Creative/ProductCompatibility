using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
