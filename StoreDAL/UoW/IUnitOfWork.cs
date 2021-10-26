using StoreDAL.Data;

namespace StoreDAL.UoW
{
    /// <summary>
    /// This interface would describe contract of 
    /// pattern Unit of Work
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <value>
        /// The <c>ProductRepository</c> property represents products' repository
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="ProductRepository"/> is a
        /// that you use for products' repository.
        /// </remarks>
        public CollectionProductRepository ProductRepository { get; }

        /// <value>
        /// The <c>UserRepository</c> property represents users' repository
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="UserRepository"/> is a
        /// that you use for users' repository.
        /// </remarks>
        public CollectionUserRepository UserRepository { get; }

        /// <value>
        /// The <c>OrderRepository</c> property represents orders' repository
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="OrderRepository"/> is a
        /// that you use for orders' repository.
        /// </remarks>
        public CollectionOrderRepository OrderRepository { get; }
    }
}
