using StoreBLL.DTO;
using System.Collections.Generic;

namespace StoreBLL.Services
{
    /// <summary>
    /// This class describes contract of 
    /// service for orders
    /// </summary>
    public partial interface IOrderService
    {
        /// <summary>
        /// This method returns list of orders
        /// </summary>
        /// <returns>Collection of orders</returns>
        IEnumerable<OrderDto> ListOfOrders();

        /// <summary>
        /// This method returns user's history of ordering
        /// </summary>
        /// <param name="login">User's login</param>
        /// <returns>Collection of orders</returns>
        IEnumerable<OrderDto> ReviewOrderHistory(string login);

        /// <summary>
        /// This method create new order
        /// </summary>
        /// <param name="order">Will created order</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool CreateNewOrder(OrderDto order);

        /// <summary>
        /// This method update order's status to "Canceled by user" in specified order
        /// </summary>
        /// <param name="orderID">Order's id</param>
        void CancelOrderByClient(string orderID);

        /// <summary>
        /// This method update order's status to "Received" in specified order
        /// </summary>
        /// <param name="orderID">Order's id</param>
        void ReceiveOrderByClient(string orderID);

        /// <summary>
        /// This method update order's status in specified order
        /// </summary>
        /// <param name="orderID">Order's id</param>
        /// <param name="status">Order's status</param>
        void UpdateStatusOrder(string orderID, OrderStatusDto status);
    }
}
