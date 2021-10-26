namespace StoreConsoleApp.UI
{
    /// <summary>
    /// This class describes context for UI
    /// </summary>
    public class MenuContext
    {
        /// <summary>
        /// This property represents UI of application
        /// </summary>
        public RoleMenuControl RoleMenu { get; private set; }

        /// <summary>
        /// Initialized <see cref="RoleMenu"/> with specified <paramref name="roleMenu"/>
        /// </summary>
        /// <param name="roleMenu">User's interface</param>
        public MenuContext(RoleMenuControl roleMenu)
        {
            RoleMenu = roleMenu;
        }

        /// <summary>
        /// This method change <see cref="RoleMenu"/> with specified <paramref name="roleMenu"/>
        /// </summary>
        /// <param name="roleMenu">User's interface</param>
        public void SetStrategy(RoleMenuControl roleMenu)
        {
            RoleMenu = roleMenu;
        }

        /// <summary>
        /// This method launches algorithm of <see cref="RoleMenu"/>
        /// </summary>
        public void Execute()
        {
            RoleMenu.ControlMenu();
        }
    }
}
