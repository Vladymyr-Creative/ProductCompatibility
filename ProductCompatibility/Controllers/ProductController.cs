using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCompatibility.Data;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using ProductCompatibility.ViewModels;

namespace ProductCompatibility.Controllers
{
    [Route("[controller]/[action]/")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _repoProd;
        private readonly IAllCategories _productCategories;
        private readonly AppDBContent _appDBContent;
        public ProductController(IRepository<Product> allProducts, IAllCategories productCategories)
        {
            _repoProd = allProducts;
            _productCategories = productCategories;
        }

        [Route("{category?}")]
        public ViewResult List(string category)
        {
            IEnumerable<Product> products = null;
            string currCategory = "";
            if (string.IsNullOrEmpty(category)) {
                products = _repoProd.All.OrderBy(i => i.Id);
            }
            else {
                products = _repoProd.All.Where(i => i.Category.Name.ToLower().Equals(category.ToLower()));
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

        [Route("{id?}")]
        public async Task<IActionResult> Single(int id)
        {
            Product product = await _repoProd.FindByIdAsync(id);
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
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid) {
                await _repoProd.AddAsync(product);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = _productCategories.AllCategories;
            return View(product);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _repoProd.FindByIdAsync(id);
            if (product != null) {
                ViewBag.Categories = _productCategories.AllCategories;
                return View(product);
            }
            return NotFound();
        }


        [Route("{id}")]
        [HttpPost]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [FromForm] Product editedProd)
        {
            if (!ModelState.IsValid) {
                ViewBag.Categories = _productCategories.AllCategories;
                editedProd.Id = id;
                return View(editedProd);
            }

            var prod = await _repoProd.FindByIdAsync(id);
            if (prod != null) {
                prod.Name = editedProd.Name;
                prod.Description = editedProd.Description;
                prod.CategoryId = editedProd.CategoryId;
                prod.Img = editedProd.Img;
                await _repoProd.UpdateAsync(editedProd);
                return RedirectToAction("Index", "Home");
            }
            return NotFound();


        }

    }
}