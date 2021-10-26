using StoreDAL.Context;
using StoreDAL.Data;

namespace StoreDAL.UoW
{
    ///<inheritdoc cref="IUnitOfWork"/>
    public partial class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext storeContext;
        private CollectionProductRepository productRepository;
        private CollectionUserRepository userRepository;
        private CollectionOrderRepository orderRepository;

        /// <summary>
        /// Initialize StoreContext
        /// </summary>
        public UnitOfWork()
        {
            storeContext = new StoreContext();
        }

        ///<inheritdoc cref="IUnitOfWork.ProductRepository"/>
        public CollectionProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new CollectionProductRepository(storeContext);
                return productRepository;
            }
        }

        ///<inheritdoc cref="IUnitOfWork.UserRepository"/>
        public CollectionUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new CollectionUserRepository(storeContext);
                return userRepository;
            }
        }

        ///<inheritdoc cref="IUnitOfWork.OrderRepository"/>
        public CollectionOrderRepository OrderRepository
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new CollectionOrderRepository(storeContext);
                return orderRepository;
            }
        }
    }
}
