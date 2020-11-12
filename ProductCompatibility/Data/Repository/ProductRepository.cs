using Microsoft.EntityFrameworkCore;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDBContent _appDBContent;

        public ProductRepository(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }

        public IEnumerable<Product> All => _appDBContent.Product.Include(c=>c.Category);

        public async Task<Product> FindByIdAsync(int id) => await _appDBContent.Product.FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Product product)
        {            
            await _appDBContent.Product.AddAsync(product);
            await _appDBContent.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _appDBContent.Product.Update(product);
            await _appDBContent.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _appDBContent.Product.Remove(product);
            await _appDBContent.SaveChangesAsync();
        }
    }
}
