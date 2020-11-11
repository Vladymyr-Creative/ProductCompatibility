using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Models
{
    public class ShopCart
    {
        private readonly AppDBContent _appDBContent;

        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDBContent>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", shopCartId);
            return new ShopCart(context) { Id = shopCartId };
        }

        public ShopCart(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }
        public string Id { get; set; }
        public List<ShopCartItem> ListShopItems { get; set; }
        public void AddToCart(Product product)
        {
            _appDBContent.ShopCartItem.Add(new ShopCartItem {
                ShopCartId = Id,
                Product = product
            });

            _appDBContent.SaveChanges();
        }

        public List<ShopCartItem> GetShopItems()
        {
            return _appDBContent.ShopCartItem.Where(c => c.ShopCartId == Id).Include(s => s.Product).ToList();
        }
    }
}
