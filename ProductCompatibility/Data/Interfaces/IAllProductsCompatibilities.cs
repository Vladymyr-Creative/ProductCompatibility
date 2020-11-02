using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Interfaces
{
    public interface IAllProductsCompatibilities
    {
        IEnumerable<ProductsCompatibility> AllProductsComapatibilities { get;}
        ProductsCompatibility GetObjectProductsCompatibility(int productID1, int productID2);
        void CreateProductsCompatibility(ProductsCompatibility productsCompatibility);
    }
}
