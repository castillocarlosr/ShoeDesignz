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
        public ShoeDesignzDbContext _context { get; }

        public InventoryManagementServices(ShoeDesignzDbContext context)
        {
            _context = context;
        }


        public async Task CreateInventory(Inventory inventory)
        {
            inventory.DiscountPrice = inventory.Price / 4;
            _context.Shoes.Add(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetInventories()
        {
            return await _context.Shoes.ToListAsync();
        }

        public async Task<Inventory> GetInventoryByID(int id)
        {
            return await _context.Shoes.FirstOrDefaultAsync(s => s.ID == id);
        }

        public Task<Inventory> GetInventoryAll(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Inventory> GetInventoryByGender(int id)
        {
            throw new NotImplementedException();
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
    }
}
