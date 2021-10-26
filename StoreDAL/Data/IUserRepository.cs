using StoreDAL.Models;
using System.Collections.Generic;

namespace StoreDAL.Data
{
    /// <summary>
    /// This class describes describe contract of 
    /// pattern Repositiry for users
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// This method returns all users
        /// </summary>
        /// <returns>Collection of users</returns>
        IEnumerable<User> GetUsers();

        /// <summary>
        /// This method returns user with specified id
        /// </summary>
        /// <param name="userID">User's id</param>
        /// <returns>User with specified id</returns>
        User GetUserByID(string userID);

        /// <summary>
        /// This method returns user with specified login
        /// </summary>
        /// <param name="login">User's login</param>
        /// <returns>User with specified login</returns>
        User GetUserByLogin(string login);

        /// <summary>
        /// This method insert user to context
        /// </summary>
        /// <param name="user">Will inserted user</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool InsertUser(User user);

        /// <summary>
        /// This method update specified user in context
        /// </summary>
        /// <param name="user">Will updated user</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool UpdateUser(User user);
    }
}
