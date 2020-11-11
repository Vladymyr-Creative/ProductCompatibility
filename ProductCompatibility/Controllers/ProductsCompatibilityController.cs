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
    public class ProductsCompatibilityController : Controller
    {
        private readonly IAllProductsCompatibilities _allProductsCompatibilities;
        private readonly IAllCompatibilities _allCompatibilities;
        
        public ProductsCompatibilityController(IAllProductsCompatibilities allProductsCompatibilities, IAllCompatibilities allCompatibilities)
        {
            _allProductsCompatibilities = allProductsCompatibilities;
            _allCompatibilities = allCompatibilities;
        }

        public IActionResult Add()
        {
            ViewBag.ProductsComapatibilities = _allProductsCompatibilities.AllProductsComapatibilities;
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductsCompatibility prodComp)
        {
            if (prodComp.Product1Id== prodComp.Product2Id) {
                throw new ApplicationException($"Product1Id and Product2Id can't be the same. Products must be different! Product1Id = {prodComp.Product1Id}, Product2Id = {prodComp.Product2Id} given.");
            }
            if (prodComp.Product1Id > prodComp.Product2Id) {
                int temp = prodComp.Product1Id;
                prodComp.Product1Id = prodComp.Product2Id;
                prodComp.Product2Id = temp;
            }

            if (ModelState.IsValid) {
                _allProductsCompatibilities.CreateProductsCompatibility(prodComp);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ProductsComapatibilities = _allProductsCompatibilities.AllProductsComapatibilities;
            return View(prodComp);
        }
    }
}
