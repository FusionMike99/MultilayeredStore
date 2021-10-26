using StoreBLL.Services;

namespace StoreBLL.Roles
{
    /// <summary>
    /// This class describes access of user's role - Guest
    /// </summary>
    public class Guest
    {
        /// <summary>
        /// This field represents products' service
        /// and can be inhereted
        /// </summary>
        protected ProductService productService;

        /// <summary>
        /// This field represents users' service
        /// and can be inhereted
        /// </summary>
        protected UserService userService;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Guest() { }

        /// <summary>
        /// This property returns <see cref="productService"/>
        /// or initialize it, if <see cref="productService"/> is <see langword="null"/>
        /// </summary>
        public ProductService ProductService
        {
            get
            {
                if (productService == null)
                    productService = new ProductService();
                return productService;
            }
        }

        /// <summary>
        /// This property returns <see cref="userService"/>
        /// or initialize it, if <see cref="userService"/> is <see langword="null"/>
        /// </summary>
        public UserService UserService
        {
            get
            {
                if (userService == null)
                    userService = new UserService();
                return userService;
            }
        }
    }
}
