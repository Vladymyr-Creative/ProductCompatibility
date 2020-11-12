using ProductCompatibility.Data.Interfaces;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly AppDBContent _appDBContent;
        private readonly ShopCart _shopCart;

        public OrderRepository(AppDBContent appDBContent, ShopCart shopCart)
        {
            _appDBContent = appDBContent;
            _shopCart = shopCart;
        }

        public IEnumerable<Order> All => throw new NotImplementedException();

        public async Task AddAsync(Order order)
        {
            order.OrderTime = DateTime.Now;
            await _appDBContent.Order.AddAsync(order);
            await _appDBContent.SaveChangesAsync();

            var items = _shopCart.ListShopItems;

            foreach (var item in items) {
                var orderDetails = new OrderDetail() {
                    ProductId = item.Product.Id,
                    OrderId = order.Id
                };
                await _appDBContent.OrderDetail.AddAsync(orderDetails);
            }
            await _appDBContent.SaveChangesAsync();
        }

        public Task DeleteAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<Order> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
