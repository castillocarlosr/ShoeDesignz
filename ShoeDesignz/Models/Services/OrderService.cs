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

        public async Task<Order> Getorder(string username)
        {
           
            Order order = await _context.Order.FirstOrDefaultAsync(e => e.UserID == username);
            order.OrderItems = await _context.OrderItems.Where(c => c.OrderID == order.ID).Include("Inventory").ToListAsync();
           
            return order;
        }
        public async Task<List<Order>> GetOrders(string username)
        {
            
            Cart cart =  await _context.Cart.FirstOrDefaultAsync(e => e.UserID == username);
            List<Order> orders =  await _context.Order.Where(e => e.UserID == cart.UserID).ToListAsync();            
          
            return orders;
        }
        public async Task<List<Inventory>> GetInventories()
        {

            return await _context.Shoes.ToListAsync();
        }



        //public async Task<OrderItems> ConvertCartItem(CartItems cartItems)
        //{

        //    OrderItems shoe = new OrderItems();
        //    shoe.OrderID = cartItems.OrderID;
        //    shoe.CartID = cartItems.CartID;
        //    shoe.InventoryID = cartItems.InventoryID;
        //    shoe.Quantity = cartItems.Quantity;

        //     _context.OrderItems.Add(shoe);
        //    await _context.SaveChangesAsync();
        //    return shoe;
        //}
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


