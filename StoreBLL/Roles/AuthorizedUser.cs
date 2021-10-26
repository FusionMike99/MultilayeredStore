using StoreBLL.Services;

namespace StoreBLL.Roles
{
    /// <summary>
    /// This class describes access of user's role - Administrator
    /// </summary>
    public partial class AuthorizedUser : Guest
    {
        private OrderService orderService;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public AuthorizedUser() { }

        /// <summary>
        /// This property returns <see cref="orderService"/>
        /// or initialize it, if <see cref="orderService"/> is <see langword="null"/>
        /// </summary>
        public OrderService OrderService
        {
            get
            {
                if (orderService == null)
                    orderService = new OrderService();
                return orderService;
            }
        }
    }
}
