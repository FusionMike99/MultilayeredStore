using StoreConsoleApp.UI;

namespace StoreSoliConsoleApp
{
    static class Program
    {
        private static void Main()
        {
            MenuContext menuContext = new MenuContext(new GuestMenuControl());
            while (menuContext.RoleMenu.IsRunning)
            {
                if (menuContext.RoleMenu is GuestMenuControl
                    && menuContext.RoleMenu.User != null)
                {
                    var user = menuContext.RoleMenu.User;
                    if (user.UserRole == "RegisteredUser")
                        menuContext.SetStrategy(new RegisteredUserMenuControl(user));
                    else
                        menuContext.SetStrategy(new AdminMenuControl(user));
                }
                else
                {
                    if (menuContext.RoleMenu.User == null)
                        menuContext.SetStrategy(new GuestMenuControl());
                }
                menuContext.Execute();
            }
        }
    }
}
