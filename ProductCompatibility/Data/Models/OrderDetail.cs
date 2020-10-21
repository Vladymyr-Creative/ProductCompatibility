
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public uint Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
