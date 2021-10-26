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
    public class UserServiceTest
    {
        private List<User> users;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            users = new List<User>()
            {
                new User("user1", "pa$$w0rd", "User1", "Userov1", "0981005060"),
                new User("user2", "pa$$w0rd", "User2", "Userov2", "0972004070"),
                new User("user3", "pa$$w0rd", "User3", "Userov3", "0963003080"),
                new User("admin", "pa$$w0rd", "Admin", "Adminov", "0911001010", UserRole.Administrator)
            };

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfigurator>();
            });
            mapper = new Mapper(configuration);
        }

        [Test]
        public void UserService_GetAll_ReturnsEqual()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            mockRepo.Setup(c => c.GetUsers()).Returns(mockContext.Object.Users);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            // Act
            var actualResult = service.ListOfUsers();
            var mappedResult = mapper.Map<IEnumerable<UserDto>, IEnumerable<User>>(actualResult);

            //Assert
            CollectionAssert.AreEquivalent(mockContext.Object.Users, mappedResult);
        }

        [Test]
        public void UserService_GetByID_ReturnsEquals()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
            var expectedUser = mockContext.Object.Users[0];

            // Act
            var actualResult = repo.GetUserByID(expectedUser.ID.ToString());

            //Assert
            Assert.AreEqual(expectedUser, actualResult, "Users are not the same");
        }

        [Test]
        public void UserService_GetByLogin_ReturnsEquals()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var expectedUser = mockContext.Object.Users[0];
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            mockRepo.Setup(c => c.GetUserByLogin(expectedUser.Login)).Returns(expectedUser);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            // Act
            var actualResult = service.SearchUserByLogin(expectedUser.Login);
            var mappedResult = mapper.Map<UserDto, User>(actualResult);

            //Assert
            Assert.AreEqual(expectedUser, mappedResult, "Users are not the same");
        }

        [TestCase("user1", "pa$$w0rd", 0)]
        [TestCase("user2", "pa$$w0rd", 1)]
        [TestCase("user3", "pa$$w0rd", 2)]
        [TestCase("admin", "pa$$w0rd", 3)]
        public void UserService_AuthorizeUser_ReturnsEquals(string login, string password, int userIndex)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var expectedUser = users[userIndex];
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            mockRepo.Setup(c => c.GetUserByLogin(login)).Returns(expectedUser);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            // Act
            var actualResult = service.AuthorizeUser(login, password);
            var mappedResult = mapper.Map<UserDto, User>(actualResult);

            //Assert
            Assert.AreEqual(expectedUser, mappedResult, "Users are not the same");
        }

        [TestCase("vsxdvsvdsv")]
        [TestCase("bdfbbdsfb")]
        [TestCase("bsdbfbbefe")]
        public void UserService_GetByID_ShouldThrowInvalidOperationException(string id)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var repo = new CollectionUserRepository(mockContext.Object);
            var expectedEx = typeof(InvalidOperationException);

            //Act
            var actualEx = Assert.Catch(() => { repo.GetUserByID(id); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("")]
        [TestCase(null)]
        public void UserService_GetByLogin_ShouldThrowArgumentNullException(string login)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);
            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { service.SearchUserByLogin(login); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("")]
        [TestCase(null)]
        public void UserService_AuthorizeUser_ShouldThrowArgumentNullException(string password)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { service.AuthorizeUser("user1", password); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("vsvdvfs")]
        [TestCase("seffesgerg")]
        [TestCase("product11")]
        public void UserService_GetByLogin_ShouldThrowInvalidOperationException(string login)
        {
            // Arrange
            var expectedEx = typeof(InvalidOperationException);
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            mockRepo.Setup(c => c.GetUserByLogin(login)).Throws<InvalidOperationException>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            //Act
            var actualEx = Assert.Catch(() => { service.SearchUserByLogin(login); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("user1", "feaefsfe", 0)]
        [TestCase("user2", "vsdvsvdfddv", 1)]
        [TestCase("user3", "vsdngfngvs", 2)]
        [TestCase("admin", "vdbvsrgdgr", 3)]
        public void UserService_AuthorizeUser_ShouldThrowArgumentException(string login, string password, int userIndex)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var arrangedUser = users[userIndex];
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            mockRepo.Setup(c => c.GetUserByLogin(login)).Returns(arrangedUser);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            var expectedEx = typeof(ArgumentException);

            //Act
            var actualEx = Assert.Catch(() => { service.AuthorizeUser(login, password); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [Test]
        public void UserService_AfterRegisterUser_ReturnsTrue()
        {
            // Arrange
            var arrangedUser = new User("user_test", "pa$$w0rd", "Tester", "Testerov", "0671545574");

            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            mockRepo.Setup(c => c.InsertUser(arrangedUser)).Returns(true);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            // Act
            var mappedUser = mapper.Map<User, UserDto>(arrangedUser);
            var actualResult = service.RegisterUser(mappedUser);

            //Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void UserService_RegisterNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { service.RegisterUser(null); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("", "afsfa", "fafwdfw", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "", "fafwdfw", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "fafwdfw", "", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "fafwdfw", "fsefesfe", "")]
        public void UserService_RegiserUserWithInvalidData_ShouldThrowArgumentException(string login, string password,
            string name, string surname, string phone)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            var arrangedUser = new User(login, password, name, surname, phone);
            var expectedEx = typeof(ArgumentException);

            //Act
            var mappedUser = mapper.Map<User, UserDto>(arrangedUser);
            var actualEx = Assert.Catch(() => { service.RegisterUser(mappedUser); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("user1")]
        [TestCase("user2")]
        [TestCase("user3")]
        [TestCase("admin")]
        public void UserService_RegisterUserWithExistsLogin_ShouldThrowInvalidOperationException(string login)
        {
            // Arrange
            var arrangedUser = new User(login, "pa$$w0rd", "Tester", "Testerov", "0671254545");

            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            mockRepo.Setup(c => c.InsertUser(arrangedUser)).Throws<InvalidOperationException>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            var expectedEx = typeof(InvalidOperationException);

            //Act
            var mappedUser = mapper.Map<User, UserDto>(arrangedUser);
            var actualEx = Assert.Catch(() => { service.RegisterUser(mappedUser); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [Test]
        public void UserService_AfterUpdateUser_ReturnsTrue()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);

            var id = mockContext.Object.Users[1].ID.ToString();
            var login = mockContext.Object.Users[1].Login;
            var arrangedUser = new User(id, login, "pa$$w0rd", "Tester", "Testerov", "0985655665");

            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            mockRepo.Setup(c => c.UpdateUser(arrangedUser)).Returns(true);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            // Act
            var mappedUser = mapper.Map<User, UserDto>(arrangedUser);
            var actualResult = service.UpdateUserProfile(mappedUser);

            //Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void UserService_UpdateUserNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            var expectedEx = typeof(ArgumentNullException);

            //Act
            var actualEx = Assert.Catch(() => { service.UpdateUserProfile(null); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }

        [TestCase("vdsvvdsv", "", "fafwdfw", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "", "fsefesfe", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "fafwdfw", "", "esfesff")]
        [TestCase("vdsvvdsv", "afsfa", "fafwdfw", "fsefesfe", "")]
        public void UserService_UpdateUserWithInvalidData_ShouldThrowArgumentException(string login, string password,
            string name, string surname, string phone)
        {
            // Arrange
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(c => c.Users).Returns(users);
            var mockRepo = new Mock<CollectionUserRepository>(mockContext.Object);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(c => c.UserRepository).Returns(mockRepo.Object);
            var service = new UserService(mockUoW.Object);

            var arrangedUser = new User(login, password, name, surname, phone);
            var expectedEx = typeof(ArgumentException);

            //Act
            var mappedUser = mapper.Map<User, UserDto>(arrangedUser);
            var actualEx = Assert.Catch(() => { service.UpdateUserProfile(mappedUser); });

            //Assert
            Assert.AreEqual(expectedEx, actualEx.GetType());
        }
    }
}
