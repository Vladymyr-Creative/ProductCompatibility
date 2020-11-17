using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IRepository<Category> _repoCat;
        private readonly IWebHostEnvironment _env;

        public ProductController(IRepository<Product> repoProd, IRepository<Category> repoCat, IWebHostEnvironment env)
        {
            _repoProd = repoProd;
            _repoCat = repoCat;
            _env = env;
        }

        [Route("{category?}")]
        public async Task<ViewResult> List(string category)
        {
            IEnumerable<Product> products = null;
            string currCategory = "";
            if (string.IsNullOrEmpty(category)) {
                products = await _repoProd.GetAllAsync();
                products = products.OrderBy(i => i.Id);
            }
            else {
                products = await _repoProd.GetAllAsync();
                products = products.Where(i => i.Category.Name.ToLower().Equals(category.ToLower()));
                currCategory = category;
            }

            var productObj = new ProductListViewModel {
                AllProducts = products,
                CurrentCategory = currCategory
            };
            return View(productObj);
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

        [Route("{id?}")]
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Categories = await _repoCat.GetAllAsync();
            ViewBag.ProdId = id;
            return View();
        }


        [HttpPost]
  //      [ValidateAntiForgeryToken]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromForm] ProductViewModel model)
        {
            
            if (ModelState.IsValid) {
                string uniqueFileName = await UploadedFile(model.Img);

                Product product = new Product{
                    Name = model.Name,
                    Description = model.Desc,
                    CategoryId = model.Cat,
                    Img = uniqueFileName,
                };

                await _repoProd.AddAsync(product);
                var prod = await _repoProd.FindByIdAsync(product.Id);
                return Ok(prod);
            }
            
            return BadRequest(ModelState);
        }

           [HttpPost("{id}")]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductViewModel editedProd)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var prod = await _repoProd.FindByIdAsync(id);
            if (prod != null) {
                prod.Name = editedProd.Name;
                prod.Description = editedProd.Desc;
                prod.CategoryId = editedProd.Cat;
                string uniqueFileName = await UploadedFile(editedProd.Img);
                if (uniqueFileName != null) prod.Img = uniqueFileName;
                await _repoProd.UpdateAsync(prod);
                var product = await _repoProd.FindByIdAsync(prod.Id);
                return Ok(product);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _repoProd.FindByIdAsync(id);
            if (prod != null) {
                await _repoProd.DeleteAsync(prod);
                return NoContent();
            }
            return NotFound();
        }

        private async Task<string> UploadedFile(IFormFile imgFile)
        {
            string uniqueFileName = null;

            if (imgFile != null) {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + imgFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                    await imgFile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }       
    }
}