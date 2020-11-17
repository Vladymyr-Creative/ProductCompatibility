using Microsoft.EntityFrameworkCore;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Repository
{
    public class ProductsCompatibilityRepository : IAllProductsCompatibilities
    {
        private readonly AppDBContent _appDBContent;

        public ProductsCompatibilityRepository(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }

        public async Task<IEnumerable<ProductsCompatibility>> GetAllAsync()
        {
            return await _appDBContent.ProductsCompatibility.Include(p => p.Compatibility).Include(p => p.Product1).Include(p => p.Product2).ToListAsync();
        }

        public async Task<ProductsCompatibility> GetByIdsAsync(int Id1, int Id2)
        {            
            return await _appDBContent.ProductsCompatibility.Where(p => p.Product1Id == Id1).Where(p => p.Product2Id == Id2).FirstOrDefaultAsync();
        }

        public async Task AddAsync(ProductsCompatibility productsCompatibility)
        {
            await _appDBContent.ProductsCompatibility.AddAsync(productsCompatibility);
            await _appDBContent.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductsCompatibility productsCompatibility)
        {
            _appDBContent.ProductsCompatibility.Update(productsCompatibility);
            await _appDBContent.SaveChangesAsync();
        }

        public Task DeleteAsync(ProductsCompatibility entity)
        {
            throw new NotImplementedException();
        }

        public Task<ProductsCompatibility> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
