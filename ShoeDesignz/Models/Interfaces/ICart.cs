using System.Threading.Tasks;

namespace ShoeDesignz.Models.Interfaces
{
   public interface ICart
    {
        // Update quantity in cart
        Task<bool> UpdateCartItems(CartItems cartItems);

        Task<CartItems> DeleteItem(int id);

        //Remove a cart item
        Task<bool> DeleteCartItems(string username);
        
        Task<Cart> GetCartForUser(string username);
    }
}
