using Microsoft.EntityFrameworkCore;
using ShoeDesignz.Data;
using ShoeDesignz.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Services
{
    public class OrderService : IOrder
    {
        private ShoeDesignzDbContext _context { get; }        


        public OrderService(ShoeDesignzDbContext context)
        {
            _context = context;
        }

        public async Task AddOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task <Order> GetOrder(int id)
        {           
            Order order = await _context.Order.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Inventory)
                                              .FirstOrDefaultAsync(o => o.ID == id);

            return order;
        }

        public async Task<List<Order>> GetOrders(string username)
        {
            Cart cart = await _context.Cart.FirstOrDefaultAsync(e => e.UserID == username);
            List<Order> orders = await _context.Order.Where(e => e.UserID == cart.UserID).ToListAsync();
            return orders;
        }

        public async Task<List<Inventory>> GetInventories()
        {

            return await _context.Shoes.ToListAsync();
        }

        public async Task<Order> CreateOrderForUser(string email)
        {

            Order order = new Order();
            order.UserID = email;
            order.Now = DateTime.Now;
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task AddOrderItem(OrderItems OrderItem)
        {
            _context.OrderItems.Add(OrderItem);
            await _context.SaveChangesAsync();
        }

      
    }
}


