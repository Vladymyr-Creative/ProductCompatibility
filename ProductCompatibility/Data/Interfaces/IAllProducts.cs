using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Interfaces
{
    public interface IAllProducts
    {
        IEnumerable<Product> Products { get; }
        Product GetObjectProducts(int productID);
    }
}
