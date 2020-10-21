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
        private readonly IAllOrders _allOrders;
        private readonly ShopCart _shopCart;

        public OrderController(IAllOrders allOrders, ShopCart shopCart)
        {
            _allOrders = allOrders;
            _shopCart = shopCart;
        }

        public IActionResult Checkout() {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            _shopCart.ListShopItems = _shopCart.GetShopItems();
            if (_shopCart.ListShopItems.Count()==0) {
                ModelState.AddModelError("", "The Cart can't be empty");
            }
            if (ModelState.IsValid) {
                _allOrders.CreateOrder(order);
                return RedirectToAction("Complete");
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
