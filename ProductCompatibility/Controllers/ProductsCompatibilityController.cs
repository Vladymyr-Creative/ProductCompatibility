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
        private readonly IAllProducts _allProducts;
        private readonly IAllCompatibilities _allCompatibilities;

        public ProductsCompatibilityController(
            IAllProductsCompatibilities allProductsCompatibilities,
            IAllProducts allProducts,
            IAllCompatibilities allCompatibilities
            )
        {
            _allProductsCompatibilities = allProductsCompatibilities;
            _allProducts = allProducts;
            _allCompatibilities = allCompatibilities;
        }

        public IActionResult Add()
        {
            ViewBag.viewModel = GetProdCompViewModel();
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductsCompatibility prodComp)
        {
            ViewBag.viewModel = GetProdCompViewModel();

            if (prodComp.Product1Id == prodComp.Product2Id) {
                return View(prodComp);
            }
            if (prodComp.Product1Id > prodComp.Product2Id) {
                int temp = prodComp.Product1Id;
                prodComp.Product1Id = prodComp.Product2Id;
                prodComp.Product2Id = temp;
            }

            if (ModelState.IsValid) {
                if (_allProductsCompatibilities.GetByIds(prodComp.Product1Id, prodComp.Product2Id) == null) {
                    _allProductsCompatibilities.Create(prodComp);
                }
                else {
                    _allProductsCompatibilities.Update(prodComp);
                }
                return RedirectToAction("Index", "Home");
            }

            return View(prodComp);
        }

        private ProductsCompatibilityViewModel GetProdCompViewModel()
        {
            return new ProductsCompatibilityViewModel {
                AllProdCompatibilities = _allProductsCompatibilities.All,
                AllProducts = _allProducts.All,
                Compatibilities = _allCompatibilities.All,
            };
        }
    }
}
