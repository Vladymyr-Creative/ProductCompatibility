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

        public IEnumerable<ProductsCompatibility> AllProductsComapatibilities => _appDBContent.ProductsCompatibility.Include(p => p.Product1).Include(p => p.Product2);

        public void CreateProduct(Product product)
        {
            _appDBContent.Product.Add(product);
            _appDBContent.SaveChanges();
        }

        public ProductsCompatibility GetObjectProductsCompatibility(int productID1, int productID2)
        {
            return _appDBContent.ProductsCompatibility.Where(p => p.Product1ID == productID1).Where(p => p.Product2ID == productID2).FirstOrDefault();
        }

        public void CreateProductsCompatibility(ProductsCompatibility productsCompatibility)
        {
            _appDBContent.ProductsCompatibility.Add(productsCompatibility);
            _appDBContent.SaveChanges();
        }
    }
}
