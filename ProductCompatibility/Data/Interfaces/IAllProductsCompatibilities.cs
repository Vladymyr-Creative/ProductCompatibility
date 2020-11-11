using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Interfaces
{
    public interface IAllProductsCompatibilities
    {
        IEnumerable<ProductsCompatibility> All { get;}
        ProductsCompatibility GetByIds(int Id1, int Id2);
        void Create(ProductsCompatibility productsCompatibility);
        void Update(ProductsCompatibility productsCompatibility);
    }
}
