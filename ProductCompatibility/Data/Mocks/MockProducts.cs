using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Mocks
{
    public class MockProducts : IAllProducts
    {
        private readonly IProductCategories _productCategory =  new MockCategories();
        public IEnumerable<Product> Products
        {
            get {
                return new List<Product> {
                    new Product { Name="Свекла", Category=_productCategory.AllCategories.First()},
                    new Product { Name="Огурцы", Category=_productCategory.AllCategories.First()},
                    new Product { Name="Молоко", Category=_productCategory.AllCategories.Last()}
                };
            }            
        }


        public Product GetObjectProducts(int productID)
        {
            throw new NotImplementedException();
        }
    }
}
