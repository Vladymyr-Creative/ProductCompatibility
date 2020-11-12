using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;

namespace ProductCompatibility.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _repoOrder;
        private readonly ShopCart _shopCart;

        public OrderController(IRepository<Order> repoOrder, ShopCart shopCart)
        {
            _repoOrder = repoOrder;
            _shopCart = shopCart;
        }

        public IActionResult Checkout() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            _shopCart.ListShopItems = _shopCart.GetShopItems();
            if (_shopCart.ListShopItems.Count() == 0) {
                ModelState.AddModelError("", "The Cart can't be empty");
            }
            if (ModelState.IsValid) {
                await _repoOrder.AddAsync(order);
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Success!";
            return View();
        }
    }
}
