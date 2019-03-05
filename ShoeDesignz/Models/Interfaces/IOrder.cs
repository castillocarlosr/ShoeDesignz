using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Interfaces
{
   public interface IOrder
    {
        // Add a new order
        Task AddOrder(Order order);

        Task UpdateOrder(Order order);

        // view order details
        //Task<List<OrderItems>> GetOrderDetails();

        //Task<OrderItems> ConvertCartItem(CartItems orderItems);

        Task AddOrderItem(OrderItems orderItem);


        Task <List<Order>> GetOrders(string username);
                

        Task <Order> GetOrder(int id);

        Task<Order> CreateOrderForUser(string email);

    }
}
