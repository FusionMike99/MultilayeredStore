using AutoMapper;
using StoreBLL.DTO;
using StoreBLL.Infrastructure;
using StoreDAL.Models;
using StoreDAL.UoW;
using System;
using System.Collections.Generic;

namespace StoreBLL.Services
{
    ///<inheritdoc cref="IOrderService"/>
    public partial class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialize <see cref="unitOfWork"/> and <see cref="mapper"/> to default
        /// </summary>
        public OrderService()
        {
            unitOfWork = new UnitOfWork();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfigurator>();
            });
            mapper = new Mapper(configuration);
        }

        /// <summary>
        /// Initialize <see cref="unitOfWork"/> to specified object
        /// and <see cref="mapper"/> to default
        /// </summary>
        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfigurator>();
            });
            mapper = new Mapper(configuration);
        }

        /// <summary>
        /// Initialize <see cref="unitOfWork"/> and <see cref="mapper"/> to specified objects
        /// </summary>
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        ///<inheritdoc cref="IOrderService.CancelOrderByClient(string)"/>
        /// <exception cref="System.ArgumentException">
        /// Thrown when status of <paramref name="orderID" /> is more or equal than 3
        /// </exception>
        public void CancelOrderByClient(string orderID)
        {
            var order = unitOfWork.OrderRepository.GetOrderByID(orderID);
            if (((byte)order.OrderStatus) >= 3)
                throw new ArgumentException("Order has been already received/competed/canceled");
            unitOfWork.OrderRepository.UpdateOrderStatus(orderID, OrderStatus.CanceledByUser);
        }

        ///<inheritdoc cref="IOrderService.CreateNewOrder(OrderDto)"/>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="order" /> is <see langword="null"/>
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="order" /> is invalid
        /// </exception>
        public bool CreateNewOrder(OrderDto order)
        {
            if (order == null)
                throw new ArgumentNullException("order", " is null");
            if (!ValidateOrder(order))
                throw new ArgumentException("Some arguments of order are not valid");
            var mappedOrder = mapper.Map<OrderDto, Order>(order);
            bool result = unitOfWork.OrderRepository.InsertOrder(mappedOrder);
            return result;
        }

        ///<inheritdoc cref="IOrderService.ListOfOrders"/>
        public IEnumerable<OrderDto> ListOfOrders()
        {
            var unmappedOrders = unitOfWork.OrderRepository.GetOrders();
            var mappedOrders = mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(unmappedOrders);
            return mappedOrders;
        }

        ///<inheritdoc cref="IOrderService.ReceiveOrderByClient(string)"/>
        /// <exception cref="System.ArgumentException">
        /// Thrown when status of <paramref name="orderID" /> is more or equal than 3
        /// </exception>
        public void ReceiveOrderByClient(string orderID)
        {
            var order = unitOfWork.OrderRepository.GetOrderByID(orderID);
            if (((byte)order.OrderStatus) >= 3)
                throw new ArgumentException("Order has been already received/competed/canceled");
            unitOfWork.OrderRepository.UpdateOrderStatus(orderID, OrderStatus.Received);
        }

        ///<inheritdoc cref="IOrderService.ReviewOrderHistory(string)"/>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="login" /> is <see langword="null"/>
        /// or empty
        /// </exception>
        public IEnumerable<OrderDto> ReviewOrderHistory(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException("login", "argument login is empty");
            var unmappedOrders = unitOfWork.OrderRepository.GetOrdersByLogin(login);
            var mappedOrders = mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(unmappedOrders);
            return mappedOrders;
        }

        ///<inheritdoc cref="IOrderService.UpdateStatusOrder(string, OrderStatusDto)"/>
        /// <exception cref="System.ArgumentException">
        /// Thrown when status of <paramref name="orderID" /> is more or equal
        /// than <paramref name="status"/>
        /// </exception>
        public void UpdateStatusOrder(string orderID, OrderStatusDto status)
        {
            var order = unitOfWork.OrderRepository.GetOrderByID(orderID);
            if (((byte)order.OrderStatus) >= (byte)status)
                throw new ArgumentException("Order status has already a higher status");
            var mappedStatus = mapper.Map<OrderStatusDto, OrderStatus>(status);
            unitOfWork.OrderRepository.UpdateOrderStatus(orderID, mappedStatus);
        }

        /// <summary>
        /// This method validate object type OrderDto
        /// </summary>
        /// <param name="order">Will validated user</param>
        /// <returns>Success of validation</returns>
        private bool ValidateOrder(OrderDto order)
        {
            if (order.OrderItems != null && order.User != null)
                return true;
            else
                return false;
        }
    }
}
