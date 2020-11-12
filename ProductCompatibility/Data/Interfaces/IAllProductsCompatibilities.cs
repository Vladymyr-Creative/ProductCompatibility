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
        Task<ProductsCompatibility> GetByIdsAsync(int Id1, int Id2);
        Task AddAsync(ProductsCompatibility productsCompatibility);
        Task UpdateAsync(ProductsCompatibility productsCompatibility);
    }
}
