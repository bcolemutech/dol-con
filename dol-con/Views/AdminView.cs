using dol_con.Utilities;
using dol_sdk.Controllers;
using dol_sdk.Enums;

namespace dol_con.Views
{
    public interface IAdminView
    {
        void Show();
    }

    public class AdminView : IAdminView
    {
        private readonly IAdminController _controller;
        private readonly IConsoleWrapper _console;

        public AdminView(IConsoleWrapper consoleWrapper, IAdminController controller)
        {
            _console = consoleWrapper;
            _controller = controller;
        }

        public void Show()
        {
            _console.Clear();
            _console.Write("Enter users email: ");
            var email = _console.ReadLine(1);
            _console.Write("Enter authority. (A)dministrator, (T)ester, or (P)layer: ");
            var authorityString = _console.ReadLine(2).ToUpper().Trim();

            var authority = authorityString switch
            {
                "A" => Authority.Admin,
                "T" => Authority.Tester,
                "P" => Authority.Player,
                _ => Authority.Player
            };

            _controller.UpdateUser(email, authority);
        }
    }
}
