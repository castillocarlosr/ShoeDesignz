using System.Threading.Tasks;

namespace ShoeDesignz.Models.Interfaces
{
   public interface ICart
    {
        // Update quantity in cart
        Task<Cart> UpdateCartItems(int id, string username);

        Task<CartItems> DeleteItem(int id);

        //Remove a cart item
        Task<bool> DeleteCartItems(string username);
        
        Task<Cart> GetCartForUser(string username);
    }
}
