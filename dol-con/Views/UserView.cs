using dol_con.Controllers;
using dol_con.Utilities;

namespace dol_con.Views
{
    public interface IUserView
    {
        void Show();
    }

    public class UserView : IUserView
    {
        private readonly IConsoleWrapper _console;
        private readonly IUserController _user;
        public UserView(IConsoleWrapper console, IUserController userController)
        {
            _console = console;
            _user = userController;
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}