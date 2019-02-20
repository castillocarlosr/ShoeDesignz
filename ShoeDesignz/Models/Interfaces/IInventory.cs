using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Interfaces
{
    public interface IInventory
    {
        //Create
        Task CreateInventory(Inventory inventory);

        //Read one Item
        Task<Inventory> GetInventoryByID(int id);

        //Read Item by Gender  Maybe use an ENUM
        Task<Inventory> GetInventoryByGender(int id);

        //Read all items
        Task<Inventory> GetInventoryAll(int id);

        //Read all items
        Task<IEnumerable<Inventory>> GetInventories();

        //Update
        Task UpdateInventory(Inventory inventory);

        //Delete
        Task DeleteInventory(int id);
    }
}
