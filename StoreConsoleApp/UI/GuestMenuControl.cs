using StoreBLL.DTO;
using StoreBLL.Roles;
using System;
using static System.Console;

namespace StoreConsoleApp.UI
{
    /// <summary>
    /// This class describes user's interface for guest
    /// </summary>
    public partial class GuestMenuControl : RoleMenuControl
    {
        private readonly Guest guest;

        /// <summary>
        /// Initialize <see cref="guest"/> to default, <see cref="RoleMenuControl.IsRunning"/>
        /// to <see langword="true"/> and <see cref="RoleMenuControl.User"/> to <see langword="null"/>
        /// </summary>
        public GuestMenuControl()
        {
            guest = new Guest();
            User = null;
            IsRunning = true;
        }

        /// <summary>
        /// This method describes controlling menu of UI.
        /// </summary>
        public override void ControlMenu()
        {
            var menuElements = new string[]
            {
                "List of products", "Find product by name",
                "Registration", "Authorization", "Exit"
            };
            DisplayMenu(menuElements);
            if (byte.TryParse(ReadLine(), out byte result))
            {
                try
                {
                    Operation operation = result switch
                    {
                        1 => DisplayProducts,
                        2 => DisplayProductByName,
                        3 => Registration,
                        4 => Authorization,
                        5 => ExitFromApp,
                        _ => DisplayUnknwonOperation
                    };
                    operation.Invoke();
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }
            }
            else
            {
                WriteLine("Inputting is not a number. Please, input number");
            }
        }

        private void DisplayProducts()
        {
            var products = guest.ProductService.ListOfProducts();
            foreach (var item in products)
            {
                WriteLine("{0} {1} {2} {3}", item.Name, item.Category, item.Description, item.Cost);
            }
        }

        private void DisplayProductByName()
        {
            string input = GetInput("Input name of product");
            var product = guest.ProductService.SearchProductByName(input);
            WriteLine("{0} {1} {2} {3}", product.Name, product.Category, product.Description, product.Cost);
        }

        private void Registration()
        {
            string login = GetInput("Input login");
            string password = GetInput("Input password");
            string name = GetInput("Input name");
            string surname = GetInput("Input surname");
            string phoneNumber = GetInput("Input phone number");
            guest.UserService.RegisterUser(new UserDto(login, password, name, surname, phoneNumber));
        }

        private void Authorization()
        {
            string login = GetInput("Input login");
            string password = GetInput("Input password");
            User = guest.UserService.AuthorizeUser(login, password);
        }
    }
}
