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

        public IEnumerable<ProductsCompatibility> All => _appDBContent.ProductsCompatibility.Include(p => p.Compatibility).Include(p => p.Product1).Include(p => p.Product2);
        
        public ProductsCompatibility GetByIds(int Id1, int Id2)
        {            
            return _appDBContent.ProductsCompatibility.Where(p => p.Product1Id == Id1).Where(p => p.Product2Id == Id2).FirstOrDefault();
        }

        public void Create(ProductsCompatibility productsCompatibility)
        {
            _appDBContent.ProductsCompatibility.Add(productsCompatibility);
            _appDBContent.SaveChanges();
        }

        public void Update(ProductsCompatibility productsCompatibility)
        {
            _appDBContent.ProductsCompatibility.Update(productsCompatibility);
            _appDBContent.SaveChanges();
        }
    }
}
