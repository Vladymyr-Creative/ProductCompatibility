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
        private readonly IAllProductsCompatibilities _repoProdComp;
        private readonly IRepository<Product> _repoProd;
        private readonly IRepository<Compatibility> _repoComp;

        public ProductsCompatibilityController(
            IAllProductsCompatibilities repoProdComp,
            IRepository<Product> repoProd,
            IRepository<Compatibility> repoComp
            )
        {
            _repoProdComp = repoProdComp;
            _repoProd = repoProd;
            _repoComp = repoComp;
        }

        public IActionResult Add()
        {
            ViewBag.viewModel = GetProdCompViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductsCompatibility prodComp)
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
                var prodCompOld = await _repoProdComp.GetByIdsAsync(prodComp.Product1Id, prodComp.Product2Id);
                if (prodCompOld == null) {
                    await _repoProdComp.AddAsync(prodComp);
                }
                else {                    
                    prodCompOld.CompatibilityId = prodComp.CompatibilityId;
                    await  _repoProdComp.UpdateAsync(prodCompOld);
                }
                return RedirectToAction("Index", "Home");
            }

            return View(prodComp);
        }

        public IActionResult Edit()
        {
            ViewBag.viewModel = GetProdCompViewModel();
            return View();
        }

        private ProductsCompatibilityViewModel GetProdCompViewModel()
        {
            return new ProductsCompatibilityViewModel {
                AllProdCompatibilities = _repoProdComp.All,
                AllProducts = _repoProd.All,
                Compatibilities = _repoComp.All,
            };
        }
    }
}
