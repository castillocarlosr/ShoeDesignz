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
   
        public async Task<Cart> DeleteCartItem(int id)
        {
            Cart cart = await _context.Cart.Include(o => o.CartItems)
                                             .ThenInclude(oi => oi.Inventory)
                                             .FirstOrDefaultAsync(o => o.ID == id);
            _context.Remove(cart);
            await _context.SaveChangesAsync();
            return cart;
            //CartItems DeleteCartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.InventoryID== id);
            //_context.CartItems.Remove(DeleteCartItem);
            //await _context.SaveChangesAsync();
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
