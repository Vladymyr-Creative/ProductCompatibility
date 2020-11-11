using Microsoft.EntityFrameworkCore;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Repository
{
    public class ProductRepository : IAllProducts
    {
        private readonly AppDBContent _appDBContent;

        public ProductRepository(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }

        public IEnumerable<Product> All => _appDBContent.Product.Include(c=>c.Category);

        public Product FindById(int id) => _appDBContent.Product.FirstOrDefault(p => p.Id == id);

        public void Create(Product product)
        {            
            _appDBContent.Product.Add(product);
            _appDBContent.SaveChanges();
        }

        public void Update(Product product)
        {
            _appDBContent.Product.Update(product);
            _appDBContent.SaveChanges();
        }
    }
}
