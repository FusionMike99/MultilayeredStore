using StoreBLL.DTO;
using static System.Console;

namespace StoreConsoleApp.UI
{
    /// <summary>
    /// This abstract class describes UI for user
    /// </summary>
    public abstract partial class RoleMenuControl
    {
        /// <summary>
        /// This property represents should the program work
        /// </summary>
        public bool IsRunning { get; protected set; }

        /// <summary>
        /// This property respresents object which type UserDto
        /// </summary>
        public UserDto User { get; protected set; }

        /// <summary>
        /// This method describes controlling menu of UI.
        /// </summary>
        /// <remarks>
        /// This method should be implemented in descendant classes.
        /// </remarks>
        public abstract void ControlMenu();

        /// <summary>
        /// This delegate describes operation
        /// </summary>
        protected delegate void Operation();

        /// <summary>
        /// This method displays menu in console window.
        /// </summary>
        /// <param name="functions">List of functions, 
        /// which which are available to the user</param>
        protected void DisplayMenu(params string[] functions)
        {
            WriteLine("List of functions:");
            for (int i = 0; i < functions.Length; i++)
            {
                WriteLine($"{i + 1}. {functions[i]};");
            }
            Write("Choose function: ");
        }

        /// <summary>
        /// This method gets user's input
        /// </summary>
        /// <param name="message">Displayed message for user</param>
        /// <returns>User's input</returns>
        protected string GetInput(string message)
        {
            Write("{0}: ", message);
            return ReadLine();
        }

        /// <summary>
        /// This method allows user to exit from application.
        /// </summary>
        protected void ExitFromApp() => IsRunning = false;

        /// <summary>
        /// This method displays message for user, 
        /// when the user enters unknown operation.
        /// </summary>
        protected void DisplayUnknwonOperation() => WriteLine("Unknown operation");
    }
}
