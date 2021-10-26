using AutoMapper;
using StoreBLL.DTO;
using StoreDAL.Models;

namespace StoreBLL.Infrastructure
{
    /// <summary>
    /// This class decribes profile for automapper
    /// </summary>
    public class MapperConfigurator : Profile
    {
        /// <summary>
        /// Set up mapping between models
        /// </summary>
        public MapperConfigurator()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(destination => destination.UserRole,
                    option => option.MapFrom(source => source.UserRole.ToString()))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ReverseMap();

            CreateMap<OrderStatus, OrderStatusDto>()
                .ReverseMap();

            CreateMap<Order, OrderDto>()
                .ReverseMap();
        }

    }
}
