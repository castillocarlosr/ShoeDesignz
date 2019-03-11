using Microsoft.EntityFrameworkCore;
using ShoeDesignz.Data;
using ShoeDesignz.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Services
{
    public class CartService : ICart
    {
        private ShoeDesignzDbContext _context { get; }
        public CartService(ShoeDesignzDbContext context)
        {
            _context = context;
        }
   
        public async Task<bool> DeleteCartItems(string username)
        {
            try
            {
                Cart cart = await _context.Cart.FirstOrDefaultAsync(c => c.UserID == username);
               
                foreach (var cartItem in cart.CartItems)
                {
                    _context.CartItems.Remove(cartItem);
                }
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<CartItems> DeleteItem(int id)
        {
            CartItems item = await _context.CartItems.Include(o => o.Inventory)                                      
                                           .FirstOrDefaultAsync(o => o.ID == id);

            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

            public async Task<bool> UpdateCartItems(CartItems cartItems)
        {
            try
            {
                 _context.CartItems.Update(cartItems);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private bool CartItemExists(int id)
        {
            return _context.Shoes.Any(e => e.ID == id);
        }

      
        public async Task<Cart> GetCartForUser(string email)
        {
            Cart cart = new Cart();
            cart.UserID = email;
            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}
