using AutoMapper;
using Moq;
using NUnit.Framework;
using StoreBLL.DTO;
using StoreBLL.Infrastructure;
using StoreBLL.Services;
using StoreDAL.Context;
using StoreDAL.Data;
using StoreDAL.Models;
using StoreDAL.UoW;
using System;
using System.Collections.Generic;

namespace StoreTestProject
{
    [TestFixture]
    public class ProductServiceTest
    {
        private List<Product> products;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            products = new List<Product>()
            {
                new Product("product1", "category1", "description1", 45.5F),
                new Product("product2", "category1", "description2", 3F),
                new Product("product3", "category2", "description3", 41F),
                new Product("product4", "category2", "description4", 89.6F),
                new Product("product5", "category3", "description5", 53F)
            };

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfigurator>();
            });
            mapper = new Mapper(configuration);
        }

        [Test]
        public void ProductService_GetAll_ReturnsEqualLength()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            mockRepo.Setup(c => c.GetProducts()).Returns(mockContext.Object.Products);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);

            // Act
            var actualResult = service.ListOfProducts();
            var mappedResult = mapper.Map<IEnumerable<ProductDto>, IEnumerable<Product>>(actualResult);

            //Assert
            CollectionAssert.AreEquivalent(mockContext.Object.Products, mappedResult);
        }

        [Test]
        public void ProductService_GetByID_ReturnsEquals()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var expectedProduct = mockContext.Object.Products[0];

            // Act
            var actualResult = repo.GetProductByID(expectedProduct.ID.ToString());

            //Assert
            Assert.AreEqual(expectedProduct, actualResult, "Products are not the same");
        }

        [Test]
        public void ProductService_GetByName_ReturnsEquals()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var expectedProduct = mockContext.Object.Products[0];
            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            mockRepo.Setup(c => c.GetProductByName(expectedProduct.Name)).Returns(expectedProduct);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);

            // Act
            var actualResult = service.SearchProductByName(expectedProduct.Name);
            var mappedResult = mapper.Map<ProductDto, Product>(actualResult);

            //Assert
            Assert.AreEqual(expectedProduct, mappedResult, "Products are not the same");
        }

        [TestCase("vsxdvsvdsv")]
        [TestCase("bdfbbdsfb")]
        [TestCase("bsdbfbbefe")]
        public void ProductService_GetByID_ShouldThrowInvalidOperationException(string id)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var repo = new CollectionProductRepository(mockContext.Object);
            var expectedEx = typeof(InvalidOperationException);

            //Act
            var actualEx = Assert.Catch(() => { repo.GetProductByID(id); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("")]
        [TestCase(null)]
        public void ProductService_GetByName_ShouldThrowArgumentNullException(string name)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);

            //Act
            var actualEx = Assert.Catch(() => { service.SearchProductByName(name); });

            //Assert
            Assert.AreEqual(typeof(ArgumentNullException), actualEx.GetType());
        }

        [TestCase("vsvdvfs")]
        [TestCase("seffesgerg")]
        [TestCase("product11")]
        public void ProductService_GetByName_ShouldThrowInvalidOperationException(string name)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            mockRepo.Setup(c => c.GetProductByName(name)).Throws<InvalidOperationException>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);

            //Act
            var actualEx = Assert.Catch(() => { service.SearchProductByName(name); });

            //Assert
            Assert.AreEqual(typeof(InvalidOperationException), actualEx.GetType());
        }

        [Test]
        public void ProductService_AfterInsert_ReturnsTrue()
        {
            // Arrange
            var arrangedProduct = new Product("ab5adb19-bf0a-4f82-8a25-d058b29fae04",
                "product11", "category6", "description11", 45.2F);

            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            mockRepo.Setup(c => c.InsertProduct(arrangedProduct)).Returns(true);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);

            // Act
            var mappedProduct = mapper.Map<Product, ProductDto>(arrangedProduct);
            var actualResult = service.AddNewProduct(mappedProduct);

            //Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void ProductService_InsertNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);

            //Act
            var actualEx = Assert.Catch(() => { service.AddNewProduct(null); });

            //Assert
            Assert.AreEqual(typeof(ArgumentNullException), actualEx.GetType());
        }

        [TestCase("", "afsfa", "fafwdfw", 56F)]
        [TestCase("awfda", "", "fafwdfw", 56F)]
        [TestCase("asffvdg", "afsfa", "", 56F)]
        [TestCase("sdvsdgs", "afsfa", "fafwdfw", 0F)]
        public void ProductService_InsertInvalidData_ShouldThrowArgumentException(string name, string category,
            string description, float cost)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);

            var arrangedProduct = new Product(name, category, description, cost);
            var expectedEx = typeof(ArgumentException);

            //Act
            var mappedProduct = mapper.Map<Product, ProductDto>(arrangedProduct);
            var actualEx = Assert.Catch(() => { service.AddNewProduct(mappedProduct); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [Test]
        public void ProductService_AfterUpdate_ReturnsTrue()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);

            var id = mockContext.Object.Products[0].ID.ToString();
            var expectedProduct = new Product(id, "product11", "category6", "description11", 45.2F);

            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            mockRepo.Setup(c => c.UpdateProduct(expectedProduct)).Returns(true);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);


            // Act
            var mappedProduct = mapper.Map<Product, ProductDto>(expectedProduct);
            var actualResult = service.UpdateProduct(mappedProduct);

            //Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void ProductService_UpdateNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);

            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { service.UpdateProduct(null); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("", "afsfa", "fafwdfw", 56F)]
        [TestCase("awfda", "", "fafwdfw", 56F)]
        [TestCase("asffvdg", "afsfa", "", 56F)]
        [TestCase("sdvsdgs", "afsfa", "fafwdfw", 0F)]
        public void ProductService_UpdateInvalidData_ShouldThrowArgumentException(string name, string category,
            string description, float cost)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Products).Returns(products);
            var mockRepo = new Mock<CollectionProductRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.ProductRepository).Returns(mockRepo.Object);
            var service = new ProductService(mockUoW.Object);

            var arrangedProduct = new Product(name, category, description, cost);
            var expectedEx = typeof(ArgumentException);

            //Act
            var mappedProduct = mapper.Map<Product, ProductDto>(arrangedProduct);
            var actualEx = Assert.Catch(() => { service.UpdateProduct(mappedProduct); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }
    }
}