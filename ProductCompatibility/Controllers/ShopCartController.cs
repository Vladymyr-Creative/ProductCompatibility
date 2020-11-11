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
        private readonly IAllProducts _prodRep;
        private readonly ShopCart _shopCart;

        public ShopCartController(IAllProducts prodRep, ShopCart shopCart)
        {
            _prodRep = prodRep;
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

        public RedirectToActionResult AddToCart(int prodId)
        {
            var item = _prodRep.Products.FirstOrDefault(i => i.Id == prodId);
            if (item!=null) {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

    }
}
