using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Interfaces
{
    interface IInventory
    {
        //Create
        Task CreateInventory(Inventory inventory);

        //Read
        Task<Inventory> GetInventory(int id);

        Task<IEnumerable<Inventory>> GetInventories();

        //Edit
        Task EditInventory(Inventory inventory);

        //Delete
        Task DeleteInventory(int id);
    }
}
