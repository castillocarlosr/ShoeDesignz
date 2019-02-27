using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Interfaces
{
    public interface IInventory
    {

        //Read one Item
        Task<Inventory> GetInventoryByID(int id);

        //Read all items
        Task<List<Inventory>> GetInventories();

        //Update
        Task UpdateInventory(Inventory inventory);

        //Delete
        Task DeleteInventory(int id);


    }
}
