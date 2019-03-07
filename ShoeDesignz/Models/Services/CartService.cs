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
                // cart.CartItems = await _context.CartItems.Where(ci => ci. == ci.ID).Include("Inventory").ToListAsync();

                //cart.CartItems = await _context.CartItems.Where(ci => ci.Cart.UserID == username).Include("Inventory").ToListAsync();
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

        public Task<List<CartItems>> SendOrder()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCart(CartItems CartItems)
        {
            _context.CartItems.Update(CartItems);
            await _context.SaveChangesAsync();          
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
