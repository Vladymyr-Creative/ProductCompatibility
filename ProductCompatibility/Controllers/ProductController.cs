using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using ProductCompatibility.ViewModels;

namespace ProductCompatibility.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAllProducts _allProducts;
        private readonly IAllCategories _productCategories;
        public ProductController(IAllProducts allProducts, IAllCategories productCategories)
        {
            _allProducts = allProducts;
            _productCategories = productCategories;
        }

        [AllowAnonymous]
        [Route("Product/List")]
        [Route("Product/List/{category}")]
        public ViewResult List(string category)
        {
            IEnumerable<Product> products = null;
            string currCategory = "";
            if (string.IsNullOrEmpty(category)) {
                products = _allProducts.Products.OrderBy(i => i.Id);
            }
            else {
                products = _allProducts.Products.Where(i => i.Category.Name.ToLower().Equals(category.ToLower()));
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

        [AllowAnonymous]
        [Route("Product/Single/{productId}")]
        public IActionResult Single(int productId)
        {
            Product product = _allProducts.Products.Where(i => i.Id == productId).FirstOrDefault();
            var productObj = new ProductListViewModel {
                Product = product
            };
            return View(productObj);
        }


        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            ViewBag.Categories = _productCategories.AllCategories;
            return View();
        }

        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid) {
                _allProducts.CreateProduct(product);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = _productCategories.AllCategories;
            return View(product);
        }
    }
}
