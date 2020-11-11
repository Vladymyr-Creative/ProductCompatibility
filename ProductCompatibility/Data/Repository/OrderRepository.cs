using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Repository
{
    public class OrderRepository : IAllOrders
    {
        private readonly AppDBContent _appDBContent;
        private readonly ShopCart _shopCart;

        public OrderRepository(AppDBContent appDBContent, ShopCart shopCart)
        {
            _appDBContent = appDBContent;
            _shopCart = shopCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderTime = DateTime.Now;
            _appDBContent.Order.Add(order);
            _appDBContent.SaveChanges();

            var items = _shopCart.ListShopItems;

            foreach (var item in items) {
                var orderDetails = new OrderDetail() {                    
                    ProductId = item.Product.Id,
                    OrderId = order.Id                    
                };
                _appDBContent.OrderDetail.Add(orderDetails);
            }
            _appDBContent.SaveChanges();
        }
    }
}
