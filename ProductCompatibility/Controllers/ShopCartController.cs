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
    public class ShopCartController : Controller
    {
        private readonly IRepository<Product> _repoProd;
        private readonly ShopCart _shopCart;

        public ShopCartController(IRepository<Product> repoProd, ShopCart shopCart)
        {
            _repoProd = repoProd;
            _shopCart = shopCart;
        }

        public ViewResult Index()
        {
            var items = _shopCart.GetShopItems();
            _shopCart.ListShopItems = items;

            var obj = new ShopCartViewModel {
                ShopCart = _shopCart
            };

            return View(obj);
        }

        public async Task<RedirectToActionResult> AddToCart(int id)
        {
            var item = await _repoProd.FindByIdAsync(id);
            if (item!=null) {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

    }
}
