using System;
using System.Collections.Generic;

namespace StoreDAL.Models
{
    /// <summary>
    /// Represent enumuration for order's statuses
    /// </summary>
    public enum OrderStatus : byte
    {
        /// <summary>
        /// Represent order's status - New
        /// </summary>
        New,
        /// <summary>
        /// Represent order's status - Payment received
        /// </summary>
        PaymentReceived,
        /// <summary>
        /// Represent order's status - Sent
        /// </summary>
        Sent,
        /// <summary>
        /// Represent order's status - Received
        /// </summary>
        Received,
        /// <summary>
        /// Represent order's status - Completed
        /// </summary>
        Completed,
        /// <summary>
        /// Represent order's status - Canceled by admin
        /// </summary>
        CanceledByAdmin,
        /// <summary>
        /// Represent order's status - Canceled by user
        /// </summary>
        CanceledByUser
    }

    /// <summary>
    /// Represent entity of data base - Order
    /// </summary>
    public partial class Order : BaseEntity
    {
        /// <value>
        /// The <c>OrderItems</c> property represents items of order
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="OrderItems"/> is a
        /// that you use for items of order.
        /// </remarks>
        public IEnumerable<OrderItem> OrderItems { get; set; }

        /// <value>
        /// The <c>DateOfOpening</c> property represents date of opening order
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="DateOfOpening"/> is a
        /// that you use for date of opening order.
        /// </remarks>
        public DateTime DateOfOpening { get; set; }

        /// <value>
        /// The <c>Total</c> property represents total sum
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Total"/> is a
        /// that you use for total sum.
        /// </remarks>
        public float Total { get; set; }

        /// <value>
        /// The <c>User</c> property represents user
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="User"/> is a
        /// that you use for user.
        /// </remarks>
        public User User { get; set; }

        /// <value>
        /// The <c>OrderStatus</c> property represents order's status
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="OrderStatus"/> is a
        /// that you use for order's status.
        /// </remarks>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Empty constructor for mapping
        /// </summary>
        public Order()
        {
            DateOfOpening = DateTime.Now;
            OrderStatus = OrderStatus.New;
        }

        /// <summary>
        /// Initialize <see cref="OrderItems"/>, <see cref="User"/>, <see cref="DateOfOpening"/>,
        /// <see cref="OrderStatus"/>
        /// </summary>
        /// <param name="orderItems">Collection of order's items</param>
        /// <param name="user">User</param>
        public Order(IEnumerable<OrderItem> orderItems, User user)
        {
            OrderItems = orderItems;
            DateOfOpening = DateTime.Now;
            User = user;
            OrderStatus = OrderStatus.New;
        }

        /// <summary>
        /// Initialize <see cref="BaseEntity.ID"/>, <see cref="OrderItems"/>, <see cref="User"/>, <see cref="DateOfOpening"/>,
        /// <see cref="OrderStatus"/>
        /// </summary>
        /// <param name="id">Order's ID</param>
        /// <param name="orderItems">Collection of order's items</param>
        /// <param name="user">User</param>
        public Order(string id, IEnumerable<OrderItem> orderItems, User user)
        {
            ID = Guid.Parse(id);
            OrderItems = orderItems;
            DateOfOpening = DateTime.Now;
            User = user;
            OrderStatus = OrderStatus.New;
        }

        /// <summary>
        /// This method using for comparison
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>Returns boolean</returns>
        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   ID == order.ID &&
                   EqualityComparer<IEnumerable<OrderItem>>.Default.Equals(OrderItems, order.OrderItems) &&
                   DateOfOpening == order.DateOfOpening &&
                   Total == order.Total &&
                   EqualityComparer<User>.Default.Equals(User, order.User) &&
                   OrderStatus == order.OrderStatus;
        }

        /// <summary>
        /// This method calculate hash code
        /// </summary>
        /// <returns>Returns integer</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, OrderItems, DateOfOpening, Total, User, OrderStatus);
        }
    }
}
