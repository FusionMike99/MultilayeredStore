using System;

namespace StoreDAL.Models
{
    /// <summary>
    /// Represent enumuration for user's roles
    /// </summary>
    public enum UserRole : byte
    {
        /// <summary>
        /// Represent registered user (store's client)
        /// </summary>
        RegisteredUser,
        /// <summary>
        /// Represent store's administrator
        /// </summary>
        Administrator
    }

    /// <summary>
    /// Represent entity of data base - User
    /// </summary>
    public partial class User : BaseEntity
    {
        /// <value>
        /// The <c>Login</c> property represents login
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Login"/> is a
        /// that you use for login.
        /// </remarks>
        public string Login { get; set; }

        /// <value>
        /// The <c>Password</c> property represents password
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Password"/> is a
        /// that you use for password.
        /// </remarks>
        public string Password { get; set; }

        /// <value>
        /// The <c>Name</c> property represents name
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Name"/> is a
        /// that you use for name.
        /// </remarks>
        public string Name { get; set; }

        /// <value>
        /// The <c>Surname</c> property represents surname
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="Surname"/> is a
        /// that you use for surname.
        /// </remarks>
        public string Surname { get; set; }

        /// <value>
        /// The <c>PhoneNumber</c> property represents phone number
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="PhoneNumber"/> is a
        /// that you use for phone number.
        /// </remarks>
        public string PhoneNumber { get; set; }

        /// <value>
        /// The <c>UserRole</c> property represents user's role
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="UserRole"/> is a
        /// that you use for user's role.
        /// </remarks>
        public UserRole UserRole { get; set; }

        /// <summary>
        /// Empty constructor for mapping
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initialize <see cref="Login"/>, <see cref="Password"/>, <see cref="Name"/>,
        /// <see cref="Surname"/>, <see cref="PhoneNumber"/>, <see cref="UserRole"/>
        /// </summary>
        public User(string login, string password, string name, string surname,
            string phoneNumber, UserRole userRole = UserRole.RegisteredUser)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            UserRole = userRole;
        }

        /// <summary>
        /// Initialize <see cref="BaseEntity.ID"/>, <see cref="Login"/>, <see cref="Password"/>, <see cref="Name"/>,
        /// <see cref="Surname"/>, <see cref="PhoneNumber"/>, <see cref="UserRole"/>
        /// </summary>
        public User(string id, string login, string password, string name, string surname,
            string phoneNumber, UserRole userRole = UserRole.RegisteredUser)
        {
            ID = Guid.Parse(id);
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            UserRole = userRole;
        }

        /// <summary>
        /// This method using for comparison
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>Returns boolean</returns>
        public override bool Equals(object obj)
        {
            return obj is User user &&
                   ID == user.ID &&
                   Login == user.Login &&
                   Password == user.Password &&
                   Name == user.Name &&
                   Surname == user.Surname &&
                   PhoneNumber == user.PhoneNumber &&
                   UserRole == user.UserRole;
        }

        /// <summary>
        /// This method calculate hash code
        /// </summary>
        /// <returns>Returns integer</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Login, Password, Name, Surname, PhoneNumber, UserRole);
        }
    }
}
