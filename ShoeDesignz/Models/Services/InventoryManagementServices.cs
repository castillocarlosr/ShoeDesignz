using Microsoft.EntityFrameworkCore;
using ShoeDesignz.Data;
using ShoeDesignz.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Services
{
    public class InventoryManagementServices : IInventory
    {
        private ShoeDesignzDbContext _context { get; }

        public InventoryManagementServices(ShoeDesignzDbContext context)
        {
            _context = context;
        }

   

        // Get all shoes 
        public async Task<List<Inventory>> GetInventories()
        {
          
            return await _context.Shoes.ToListAsync();
        }
     
   
        public async Task UpdateInventory(Inventory inventory)
        {
            inventory.DiscountPrice = inventory.Price / 4;
            _context.Shoes.Update(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInventory(int id)
        {
            Inventory inventory = _context.Shoes.FirstOrDefault(shoe => shoe.ID == id);
            _context.Shoes.Remove(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task<Inventory> GetInventoryByID(int id)
        {
            return await _context.Shoes.FirstOrDefaultAsync(e => e.ID == id);
        }

        private bool ShoeExists(int id)
        {
            return _context.Shoes.Any(e => e.ID == id);
        }

        public async Task AddCartItem(CartItems CartItem)
        {
            _context.CartItems.Add(CartItem);
             await _context.SaveChangesAsync();
        }
    }
}
