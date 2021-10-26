using AutoMapper;
using StoreBLL.DTO;
using StoreBLL.Infrastructure;
using StoreDAL.Models;
using StoreDAL.UoW;
using System;
using System.Collections.Generic;

namespace StoreBLL.Services
{
    ///<inheritdoc cref="IProductService"/>
    public partial class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialize <see cref="unitOfWork"/> and <see cref="mapper"/> to default
        /// </summary>
        public ProductService()
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
        public ProductService(IUnitOfWork unitOfWork)
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
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        ///<inheritdoc cref="IProductService.AddNewProduct(ProductDto)"/>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="product" /> is <see langword="null"/>
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="product" /> is invalid
        /// </exception>
        public bool AddNewProduct(ProductDto product)
        {
            if (product == null)
                throw new ArgumentNullException("product", " is null");
            if (!ValidateProduct(product))
                throw new ArgumentException("Some arguments of product are not valid");
            var mappedProduct = mapper.Map<ProductDto, Product>(product);
            bool result = unitOfWork.ProductRepository.InsertProduct(mappedProduct);
            return result;
        }

        ///<inheritdoc cref="IProductService.ListOfProducts"/>
        public IEnumerable<ProductDto> ListOfProducts()
        {
            var unmappedProducts = unitOfWork.ProductRepository.GetProducts();
            var mappedProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(unmappedProducts);
            return mappedProducts;
        }

        ///<inheritdoc cref="IProductService.SearchProductByName(string)"/>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="name" /> is <see langword="null"/>
        /// or empty
        /// </exception>
        public ProductDto SearchProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name", "Empty argument name");
            var unmappedProduct = unitOfWork.ProductRepository.GetProductByName(name);
            var mappedProduct = mapper.Map<Product, ProductDto>(unmappedProduct);
            return mappedProduct;
        }

        ///<inheritdoc cref="IProductService.UpdateProduct(ProductDto)"/>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="product" /> is <see langword="null"/>
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="product" /> is invalid
        /// </exception>
        public bool UpdateProduct(ProductDto product)
        {
            if (product == null)
                throw new ArgumentNullException("product", " is null");
            if (!ValidateProduct(product))
                throw new ArgumentException("Some arguments of product are not valid");
            var mappedProduct = mapper.Map<ProductDto, Product>(product);
            bool result = unitOfWork.ProductRepository.UpdateProduct(mappedProduct);
            return result;
        }

        /// <summary>
        /// This method validate object type ProductDto
        /// </summary>
        /// <param name="product">Will validated product</param>
        /// <returns>Success of validation</returns>
        private bool ValidateProduct(ProductDto product)
        {
            if (!string.IsNullOrEmpty(product.Name) && !string.IsNullOrEmpty(product.Category)
                && !string.IsNullOrEmpty(product.Description) && product.Cost > 0)
                return true;
            else
                return false;
        }
    }
}
