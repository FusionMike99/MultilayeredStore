using StoreBLL.DTO;
using StoreBLL.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace StoreConsoleApp.UI
{
    /// <summary>
    /// This class describes user's interface for registered user
    /// </summary>
    public class RegisteredUserMenuControl : RoleMenuControl
    {
        private readonly AuthorizedUser authorized;

        /// <summary>
        /// Initialize <see cref="authorized"/> to default, <see cref="RoleMenuControl.IsRunning"/>
        /// to <see langword="true"/> and <see cref="RoleMenuControl.User"/> to <see langword="null"/>
        /// </summary>
        public RegisteredUserMenuControl(UserDto user)
        {
            authorized = new AuthorizedUser();
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
                "Create order", "Cancel order", "View history",
                "Receive order", "Update profile", "Sign out", "Exit"
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
                        4 => CancelingOrder,
                        5 => ViewHistoryOfOrders,
                        6 => ReceiveOrder,
                        7 => UpdatingProfile,
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
            var products = authorized.ProductService.ListOfProducts();
            foreach (var item in products)
            {
                WriteLine("{0} {1} {2} {3}", item.Name, item.Category, item.Description, item.Cost);
            }
        }

        private void DisplayProductByName()
        {
            string input = GetInput("Input name of product");
            var product = authorized.ProductService.SearchProductByName(input);
            WriteLine("{0} {1} {2} {3}", product.Name, product.Category, product.Description, product.Cost);
        }

        private void CreatingOrder()
        {
            var products = authorized.ProductService.ListOfProducts().ToList();
            for (int i = 0; i < products.Count; i++)
            {
                WriteLine("{0} {1} {2} {3}", products[i].Name, products[i].Category,
                    products[i].Description, products[i].Cost);
            }

            List<OrderItemDto> items = new List<OrderItemDto>();
            while (true)
            {
                string input = GetInput("Input name of product or input \"finish\"");
                if (input == "finish")
                    break;
                ProductDto product = null;
                try
                {
                    product = authorized.ProductService.SearchProductByName(input);
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }

                input = GetInput("Input count of products");
                if (!int.TryParse(input, out int countOfProduct))
                {
                    WriteLine("Invalid input, you should input number");
                    continue;
                }
                items.Add(new OrderItemDto(product, countOfProduct));
            }

            var order = new OrderDto(items, User);
            authorized.OrderService.CreateNewOrder(order);
        }

        private void CancelingOrder()
        {
            var orders = authorized.OrderService.ReviewOrderHistory(User.Login).ToList();
            var collectionOfIds = new List<Guid>();
            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                WriteLine($"{i + 1}. Date: {order.DateOfOpening}; Status: {order.OrderStatus}");
                collectionOfIds.Add(order.ID);
            }
            string input = GetInput("Input number of order");
            if (!int.TryParse(input, out int numberOfId))
            {
                WriteLine("Invalid number");
                return;
            }
            try
            {
                authorized.OrderService.CancelOrderByClient(collectionOfIds[numberOfId - 1].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                WriteLine("Input wrong number");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        private void ViewHistoryOfOrders()
        {
            var orders = authorized.OrderService.ReviewOrderHistory(User.Login).ToList();
            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                WriteLine($"{i + 1}. Date: {order.DateOfOpening}; Status: {order.OrderStatus}:");
                foreach (var item in order.OrderItems)
                {
                    WriteLine($"\t{item.Product.Name} - {item.Amount} - {item.Cost}");
                }
            }
        }

        private void ReceiveOrder()
        {
            var orders = authorized.OrderService.ReviewOrderHistory(User.Login).ToList();
            var collectionOfIds = new List<Guid>();
            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                WriteLine($"{i + 1}. Date: {order.DateOfOpening}; Status: {order.OrderStatus}");
                collectionOfIds.Add(order.ID);
            }
            string input = GetInput("Input number of order");
            if (!int.TryParse(input, out int numberOfId))
            {
                WriteLine("Invalid number");
                return;
            }
            try
            {
                authorized.OrderService.ReceiveOrderByClient(collectionOfIds[numberOfId - 1].ToString());
            }
            catch (IndexOutOfRangeException)
            {
                WriteLine("Input wrong number");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        private void UpdatingProfile()
        {
            string password = GetInput("Input password");
            string fullname = GetInput("Input name and surname");
            string phoneNumber = GetInput("Input phone number");
            var updatableUser = new UserDto(User.ID.ToString(), User.Login, password, fullname, phoneNumber);
            authorized.UserService.UpdateUserProfile(updatableUser);
        }
    }
}
