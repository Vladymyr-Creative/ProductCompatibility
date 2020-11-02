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
            if (prodComp.Product1ID== prodComp.Product2ID) {
                throw new ApplicationException($"Product1ID and Product2ID can't be the same. Products must be different! Product1ID = {prodComp.Product1ID}, Product2ID = {prodComp.Product2ID} given.");
            }
            if (prodComp.Product1ID > prodComp.Product2ID) {
                int temp = prodComp.Product1ID;
                prodComp.Product1ID = prodComp.Product2ID;
                prodComp.Product2ID = temp;
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
