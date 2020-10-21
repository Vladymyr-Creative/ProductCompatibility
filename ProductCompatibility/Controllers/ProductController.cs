using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using ProductCompatibility.ViewModels;

namespace ProductCompatibility.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAllProducts _allProducts;
        private readonly IProductCategories _productCategories;
        public ProductController(IAllProducts allProducts, IProductCategories productCategories)
        {
            _allProducts = allProducts;
            _productCategories = productCategories;
        }


        [Route("Product/List")]
        [Route("Product/List/{category}")]
        public ViewResult List(string category)
        {
            IEnumerable<Product> products = null;
            string currCategory = "";
            if (string.IsNullOrEmpty(category)) {
                products = _allProducts.Products.OrderBy(i => i.ID);
            }
            else {
                if (string.Equals("alkaline", category, StringComparison.OrdinalIgnoreCase)) {
                    products = _allProducts.Products.Where(i => i.Category.Name.Equals("Щелочные"));
                }
                else if (string.Equals("acidic", category, StringComparison.OrdinalIgnoreCase)) {
                    products = _allProducts.Products.Where(i => i.Category.Name.Equals("Кислотные"));
                }
                else if (string.Equals("neutral", category, StringComparison.OrdinalIgnoreCase)) {
                    products = _allProducts.Products.Where(i => i.Category.Name.Equals("Нейтральные"));
                }
                currCategory = category;
            }
            var productObj = new ProductListViewModel {
                AllProducts = products,
                CurrentCategory = currCategory
            };
            return View(productObj);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Product/Single/{productID}")]
        public IActionResult Single(int productID)
        {
            Product product = _allProducts.Products.Where(i => i.ID == productID).FirstOrDefault();
            var productObj = new ProductListViewModel {                
                Product = product                
            };
            return View(productObj);            
        }

        public IActionResult Add()
        {
            ViewBag.Categories = _productCategories.AllCategories;
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {       
            if (ModelState.IsValid) {
                _allProducts.CreateProduct(product);
                return RedirectToAction("Index", "Home");
            }

            return View(product);
        }
    }
}
