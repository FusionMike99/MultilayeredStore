using System;

namespace StoreBLL.DTO
{
    /// <summary>
    /// Represent data transform object - User
    /// </summary>
    public partial class UserDto
    {
        /// <value>
        /// The <c>ID</c> property represents id
        /// for this instance.
        /// </value>
        /// <remarks>
        /// The <see cref="ID"/> is a
        /// that you use for id.
        /// </remarks>
        public Guid ID { get; set; }

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
        public string UserRole { get; set; }

        /// <summary>
        /// Empty constructor for mapping
        /// </summary>
        public UserDto()
        {
        }

        /// <summary>
        /// Initialize <see cref="Login"/>, <see cref="Password"/>, <see cref="Name"/>,
        /// <see cref="Surname"/>, <see cref="PhoneNumber"/>, <see cref="UserRole"/>
        /// </summary>
        public UserDto(string login, string password, string name, string surname,
            string phoneNumber)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Initialize <see cref="ID"/>, <see cref="Login"/>, <see cref="Password"/>, <see cref="Name"/>,
        /// <see cref="Surname"/>, <see cref="PhoneNumber"/>, <see cref="UserRole"/>
        /// </summary>
        public UserDto(string id, string login, string password, string name, string surname,
            string phoneNumber)
        {
            ID = Guid.Parse(id);
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// This method using for comparison
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>Returns boolean</returns>
        public override bool Equals(object obj)
        {
            return obj is UserDto user &&
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
