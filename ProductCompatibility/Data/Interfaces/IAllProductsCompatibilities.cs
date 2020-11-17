using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Interfaces
{
    public interface IAllProductsCompatibilities:IRepository<ProductsCompatibility>
    {
        Task<ProductsCompatibility> GetByIdsAsync(int Id1, int Id2);
    }
}
