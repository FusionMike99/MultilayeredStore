using StoreDAL.Context;
using StoreDAL.Models;
using System;
using System.Collections.Generic;

namespace StoreDAL.Data
{
    ///<inheritdoc cref="IUserRepository"/>
    public partial class CollectionUserRepository : IUserRepository
    {
        private readonly StoreContext context;

        /// <summary>
        /// Initialize context
        /// </summary>
        /// <param name="context">Store's context</param>
        public CollectionUserRepository(StoreContext context)
        {
            this.context = context;
        }

        /// <inheritdoc cref="IUserRepository.GetUserByID(string)"/>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when isn't found product with specified <paramref name="userID" />
        /// </exception>
        public virtual User GetUserByID(string userID)
        {
            var foundUser = context.Users.Find(item => item.ID.ToString() == userID);
            if (foundUser == null)
                throw new InvalidOperationException("Not found user with that ID");
            return foundUser;
        }

        ///<inheritdoc cref="IUserRepository.GetUserByLogin(string)"/>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when isn't found product with specified <paramref name="login" />
        /// </exception>
        public virtual User GetUserByLogin(string login)
        {
            var foundUser = context.Users.Find(item => item.Login == login);
            if (foundUser == null)
                throw new InvalidOperationException("Not found user with that login");
            return foundUser;
        }

        ///<inheritdoc cref="IUserRepository.GetUsers"/>
        public virtual IEnumerable<User> GetUsers()
        {
            return context.Users;
        }

        ///<inheritdoc cref="IUserRepository.InsertUser(User)"/>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when exists user with specified user's login
        /// </exception>
        public virtual bool InsertUser(User user)
        {
            if (context.Users.Exists(item => item.Login == user.Login))
                throw new InvalidOperationException("User with same login is already exist");
            context.Users.Add(user);
            bool result = context.Users.Contains(user);
            return result;
        }

        ///<inheritdoc cref="IUserRepository.UpdateUser(User)"/>
        public virtual bool UpdateUser(User user)
        {
            var updatedUser = GetUserByID(user.ID.ToString());
            updatedUser.Name = user.Name;
            updatedUser.Surname = user.Surname;
            updatedUser.PhoneNumber = user.PhoneNumber;
            updatedUser.Password = user.Password;
            bool result = context.Users.Contains(updatedUser);
            return result;
        }
    }
}
