using StoreBLL.DTO;
using System.Collections.Generic;

namespace StoreBLL.Services
{
    /// <summary>
    /// This class describes describe contract of 
    /// service for users
    /// </summary>
    public partial interface IUserService
    {
        /// <summary>
        /// This method returns list of users
        /// </summary>
        /// <returns>Collection of users</returns>
        IEnumerable<UserDto> ListOfUsers();

        /// <summary>
        /// This method searches user with specified login
        /// </summary>
        /// <param name="login">User's login</param>
        /// <returns>User with specified login</returns>
        UserDto SearchUserByLogin(string login);

        /// <summary>
        /// This method register user
        /// </summary>
        /// <param name="user">Will registered user</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool RegisterUser(UserDto user);

        /// <summary>
        /// This method authorize user
        /// </summary>
        /// <param name="login">User's login</param>
        /// <param name="password">User's password</param>
        /// <returns>Authorized user</returns>
        UserDto AuthorizeUser(string login, string password);

        /// <summary>
        /// This method update specified user's profile
        /// </summary>
        /// <param name="user">Will updated user's profile</param>
        /// <returns>Success of the operation (boolean)</returns>
        bool UpdateUserProfile(UserDto user);
    }
}
