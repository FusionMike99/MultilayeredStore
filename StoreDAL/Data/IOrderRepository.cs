using StoreDAL.Models;
using System.Collections.Generic;

namespace StoreDAL.Data
{
    /// <summary>
    /// This class describes contract of 
    /// pattern Repositiry for orders
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// This method returns all orders
        /// </summary>
        /// <returns>Collection of orders</returns>
        IEnumerable<Order> GetOrders();

        /// <summary>
        /// This method returns orders with specified user's login
        /// </summary>
        /// <param name="login">User's login</param>
        /// <returns>Collection of orders</returns>
        IEnumerable<Order> GetOrdersByLogin(string login);

        /// <summary>
        /// This method returns order with specified id
        /// </summary>
        /// <param name="orderID">Order's id</param>
        /// <returns>Order with specified id</returns>
        Order GetOrderByID(string orderID);

        /// <summary>
        /// This method insert order to context
        /// </summary>
        /// <param name="order">Will inserted order</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool InsertOrder(Order order);

        /// <summary>
        /// This method update order's status in specified order
        /// </summary>
        /// <param name="orderID">Order's id</param>
        /// <param name="orderStatus">Order's status</param>
        void UpdateOrderStatus(string orderID, OrderStatus orderStatus);
    }
}
