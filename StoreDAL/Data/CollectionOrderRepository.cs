using StoreDAL.Context;
using StoreDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreDAL.Data
{
    ///<inheritdoc cref="IOrderRepository"/>
    public partial class CollectionOrderRepository : IOrderRepository
    {
        private readonly StoreContext context;

        /// <summary>
        /// Initialize context
        /// </summary>
        /// <param name="context">Store's context</param>
        public CollectionOrderRepository(StoreContext context)
        {
            this.context = context;
        }

        ///<inheritdoc cref="IOrderRepository.GetOrderByID(string)"/>
        public virtual Order GetOrderByID(string orderID)
        {
            var foundOrder = context.Orders.Find(item => item.ID.ToString() == orderID);
            if (foundOrder == null)
                throw new InvalidOperationException("Not found order with that ID");
            return foundOrder;
        }

        ///<inheritdoc cref="IOrderRepository.GetOrders"/>
        public virtual IEnumerable<Order> GetOrders()
        {
            return context.Orders;
        }

        ///<inheritdoc cref="IOrderRepository.GetOrdersByLogin(string)"/>
        public virtual IEnumerable<Order> GetOrdersByLogin(string login)
        {
            return context.Orders.Where(item => item.User.Login == login);
        }

        ///<inheritdoc cref="IOrderRepository.InsertOrder(Order)"/>
        public virtual bool InsertOrder(Order order)
        {
            context.Orders.Add(order);
            bool result = context.Orders.Exists(o => o.ID == order.ID);
            return result;
        }

        ///<inheritdoc cref="IOrderRepository.UpdateOrderStatus(string, OrderStatus)"/>
        public virtual void UpdateOrderStatus(string orderID, OrderStatus orderStatus)
        {
            var updatedOrder = GetOrderByID(orderID);
            updatedOrder.OrderStatus = orderStatus;
        }
    }
}
