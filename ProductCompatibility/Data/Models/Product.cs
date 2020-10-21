using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public string Img { get; set; }
        public virtual Category Category { get; set; }
    }
}
