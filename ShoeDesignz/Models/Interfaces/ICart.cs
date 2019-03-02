using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Interfaces
{
   public interface ICart
    {

        // Update quantity in cart
        Task UpdateCart(CartItems CartItems);

        //Remove a cart item
        Task DeleteCartItem(int id);

        // Complete an order
        Task<List<CartItems>> SendOrder();
    }
}
