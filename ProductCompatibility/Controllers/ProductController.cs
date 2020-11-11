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
    [Route("[controller]/[action]/")]
    public class ProductController : Controller
    {
        private readonly IAllProducts _allProducts;
        private readonly IAllCategories _productCategories;
        public ProductController(IAllProducts allProducts, IAllCategories productCategories)
        {
            _allProducts = allProducts;
            _productCategories = productCategories;
        }

        [Route("{category?}")]
        public ViewResult List(string category)
        {
            IEnumerable<Product> products = null;
            string currCategory = "";
            if (string.IsNullOrEmpty(category)) {
                products = _allProducts.All.OrderBy(i => i.Id);
            }
            else {
                products = _allProducts.All.Where(i => i.Category.Name.ToLower().Equals(category.ToLower()));
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

        [Route("{productId?}")]
        public IActionResult Single(int productId)
        {
            Product product = _allProducts.All.Where(i => i.Id == productId).FirstOrDefault();
            var productObj = new ProductListViewModel {
                Product = product
            };
            return View(productObj);
        }


        //[Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            ViewBag.Categories = _productCategories.AllCategories;
            return View();
        }

        
        [HttpPost]
       // [Authorize(Roles = "admin")]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid) {
                _allProducts.Create(product);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = _productCategories.AllCategories;
            return View(product);
        }

        [Route("{productId}")]
        public IActionResult Edit(int productId)
        {
            var product = _allProducts.FindById(productId);
            if (product == null) {
                return NotFound();
            }
            ViewBag.Categories = _productCategories.AllCategories;
            return View(product);
        }


        [HttpPost]
        // [Authorize(Roles = "admin")]
        public IActionResult Editing(Product product)
        {
            if (ModelState.IsValid) {
                _allProducts.Update(product);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = _productCategories.AllCategories;
            return View(product);
        }
    }
}