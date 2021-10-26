using StoreBLL.DTO;
using StoreBLL.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace StoreConsoleApp.UI
{
    /// <summary>
    /// This class describes user's interface for administrator
    /// </summary>
    public class AdminMenuControl : RoleMenuControl
    {
        private readonly AuthorizedUser admin;

        /// <summary>
        /// Initialize <see cref="admin"/> to default, <see cref="RoleMenuControl.IsRunning"/>
        /// to <see langword="true"/> and <see cref="RoleMenuControl.User"/> to <see langword="null"/>
        /// </summary>
        public AdminMenuControl(UserDto user)
        {
            admin = new AuthorizedUser();
            IsRunning = true;
            User = user;
        }

        /// <summary>
        /// This method describes controlling menu of UI.
        /// </summary>
        public override void ControlMenu()
        {
            var menuElements = new string[]
            {
                "List of products", "Find product by name",
                "Create order", "Update user profile", "Add product",
                "Update product", "Update order status", "Sign out", "Exit"
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
                        3 => CreatingOrder,
                        4 => UpdatingUserProfile,
                        5 => CreatingProduct,
                        6 => UpdatingProduct,
                        7 => UpdatingStatusOfOrder,
                        8 => () => User = null,
                        9 => ExitFromApp,
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
            var products = admin.ProductService.ListOfProducts();
            foreach (var item in products)
            {
                WriteLine("{0} {1} {2} {3}", item.Name, item.Category, item.Description, item.Cost);
            }
        }

        private void DisplayProductByName()
        {
            string input = GetInput("Input name of product");
            var product = admin.ProductService.SearchProductByName(input);
            WriteLine("{0} {1} {2} {3}", product.Name, product.Category, product.Description, product.Cost);
        }

        private void CreatingOrder()
        {
            var products = admin.ProductService.ListOfProducts().ToList();
            for (int i = 0; i < products.Count; i++)
            {
                WriteLine("{0} {1} {2} {3}", products[i].Name, products[i].Category,
                    products[i].Description, products[i].Cost);
            }

            var items = new List<OrderItemDto>();
            while (true)
            {
                string input = GetInput("Input name of product or input \"finish\"");
                if (input == "finish")
                    break;
                ProductDto product = null;
                try
                {
                    product = admin.ProductService.SearchProductByName(input);
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }

                input = GetInput("Input count of products or input");
                if (!int.TryParse(input, out int countOfProduct))
                {
                    WriteLine("Invalid input, you should input number");
                    continue;
                }
                items.Add(new OrderItemDto(product, countOfProduct));
            }

            var order = new OrderDto(items, User);
            admin.OrderService.CreateNewOrder(order);
        }

        private void UpdatingUserProfile()
        {
            var users = admin.UserService.ListOfUsers().ToList();
            for (int i = 0; i < users.Count; i++)
            {
                var item = users[i];
                WriteLine($"{i + 1}. {item.Login} - {item.Name} {item.Surname}");
            }
            string login = GetInput("Input login");
            var foundUser = admin.UserService.SearchUserByLogin(login);
            string fullname = GetInput("Input name and surname");
            string phoneNumber = GetInput("Input phone number");
            var updatableUser = new UserDto(foundUser.ID.ToString(), foundUser.Login, foundUser.Password,
                fullname, phoneNumber);
            admin.UserService.UpdateUserProfile(updatableUser);
        }

        private void CreatingProduct()
        {
            string name = GetInput("Input name");
            string category = GetInput("Input category");
            string description = GetInput("Input description");
            string costText = GetInput("Input cost");
            if (!float.TryParse(costText, out float cost))
            {
                WriteLine("Invalid float number");
                return;
            }
            admin.ProductService.AddNewProduct(new ProductDto(name, category, description, cost));
        }

        private void UpdatingProduct()
        {
            var products = admin.ProductService.ListOfProducts().ToList();
            for (int i = 0; i < products.Count; i++)
            {
                WriteLine("{0} {1}", products[i].Name, products[i].Category);
            }
            string input = GetInput("Input name of product");
            var foundProduct = admin.ProductService.SearchProductByName(input);
            string name = GetInput("Input updated name");
            string category = GetInput("Input updated category");
            string description = GetInput("Input updated description");
            string costText = GetInput("Input updated cost");
            if (!float.TryParse(costText, out float cost))
            {
                WriteLine("Invalid float number");
                return;
            }
            admin.ProductService.UpdateProduct(new ProductDto(foundProduct.ID.ToString(), name,
               category, description, cost));
        }

        private void UpdatingStatusOfOrder()
        {
            var orders = admin.OrderService.ListOfOrders().ToList();
            var collectionOfIds = new List<Guid>();
            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                WriteLine($"{i + 1}. Date: {order.DateOfOpening}; Client: {order.User.Name} {order.User.Surname}; Status: {order.OrderStatus}");
                collectionOfIds.Add(order.ID);
            }
            string input = GetInput("Input number of order");
            if (!int.TryParse(input, out int numberOfId))
            {
                WriteLine("Invalid number");
                return;
            }
            WriteLine("Statuses:");
            WriteLine("\t1. Payment received");
            WriteLine("\t2. Order sent");
            WriteLine("\t4. Order completed");
            WriteLine("\t5. Order canceled");
            input = GetInput("Input number of status");
            if (!byte.TryParse(input, out byte numberOfStatus))
            {
                WriteLine("Invalid number");
                return;
            }
            try
            {
                admin.OrderService.UpdateStatusOrder(collectionOfIds[numberOfId - 1].ToString(), (OrderStatusDto)numberOfStatus);
            }
            catch (IndexOutOfRangeException)
            {
                WriteLine("Input wrong number");
            }
        }
    }
}
