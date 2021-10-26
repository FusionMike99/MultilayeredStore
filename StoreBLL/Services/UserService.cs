using AutoMapper;
using StoreBLL.DTO;
using StoreBLL.Infrastructure;
using StoreDAL.Models;
using StoreDAL.UoW;
using System;
using System.Collections.Generic;

namespace StoreBLL.Services
{
    ///<inheritdoc cref="IUserService"/>
    public partial class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialize <see cref="unitOfWork"/> and <see cref="mapper"/> to default
        /// </summary>
        public UserService()
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
        public UserService(IUnitOfWork unitOfWork)
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
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        ///<inheritdoc cref="IUserService.AuthorizeUser(string, string)"/>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="password" /> is <see langword="null"/>
        /// or empty
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="password" /> is wrong
        /// </exception>
        public UserDto AuthorizeUser(string login, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password", "Empty argument password");
            var foundUser = unitOfWork.UserRepository.GetUserByLogin(login);
            var mappedUser = mapper.Map<User, UserDto>(foundUser);
            if (mappedUser.Password != password)
                throw new ArgumentException("Wrong password");
            return mappedUser;
        }

        ///<inheritdoc cref="IUserService.ListOfUsers"/>
        public IEnumerable<UserDto> ListOfUsers()
        {
            var unmappedUsers = unitOfWork.UserRepository.GetUsers();
            var mappedUsers = mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(unmappedUsers);
            return mappedUsers;
        }

        ///<inheritdoc cref="IUserService.RegisterUser(UserDto)"/>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="user" /> is <see langword="null"/>
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="user" /> is invalid
        /// </exception>
        public bool RegisterUser(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException("user", " is null");
            if (!ValidateUser(user))
                throw new ArgumentException("Some arguments of user are not valid");
            var mappedUser = mapper.Map<UserDto, User>(user);
            bool result = unitOfWork.UserRepository.InsertUser(mappedUser);
            return result;
        }

        ///<inheritdoc cref="IUserService.SearchUserByLogin(string)"/>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="login" /> is <see langword="null"/>
        /// or empty
        /// </exception>
        public UserDto SearchUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException("login", "Empty argument login");
            var unmappedUser = unitOfWork.UserRepository.GetUserByLogin(login);
            var mappedUser = mapper.Map<User, UserDto>(unmappedUser);
            return mappedUser;
        }

        ///<inheritdoc cref="IUserService.UpdateUserProfile(UserDto)"/>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="user" /> is <see langword="null"/>
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="user" /> is invalid
        /// </exception>
        public bool UpdateUserProfile(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException("user", " is null");
            if (!ValidateUser(user))
                throw new ArgumentException("Some arguments of user are not valid");
            var mappedUser = mapper.Map<UserDto, User>(user);
            bool result = unitOfWork.UserRepository.UpdateUser(mappedUser);
            return result;
        }

        /// <summary>
        /// This method validate object type UserDto
        /// </summary>
        /// <param name="user">Will validated order</param>
        /// <returns>Success of validation</returns>
        private bool ValidateUser(UserDto user)
        {
            if (!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password)
                && !string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Surname)
                && !string.IsNullOrEmpty(user.PhoneNumber))
                return true;
            else
                return false;
        }
    }
}
