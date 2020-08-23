using System.Threading.Tasks;
using dol_con.Services;
using dol_con.Utilities;

namespace dol_con.Scenes
{
    public interface ITitle
    {
        Task Show(bool test);
    }

    public class Title : ITitle
    {
        private readonly IConsoleWrapper _console;
        private readonly ISecurityService _security;
        private readonly IUserService _user;

        public Title(IConsoleWrapper console, ISecurityService securityService, IUserService userService)
        {
            _console = console;
            _security = securityService;
            _user = userService;
        }

        public async Task Show(bool test = false)
        {
            if (test)
            {
                return;
            }
            _console.WriteLine("·▄▄▄▄        ▄▄▌  ");
            _console.WriteLine("██· ██  ▄█▀▄ ██•  ");
            _console.WriteLine("▐█▪ ▐█▌▐█▌.▐▌██ ▪ ");
            _console.WriteLine("██. ██ ▐█▌.▐▌▐█▌ ▄");
            _console.WriteLine("▀▀▀▀▀•  ▀█▄▀▪.▀▀▀ ");
            _console.WriteLine("");
            _console.WriteLine("Login to proceed.");
            _console.Write("Enter email: ");
            var user = _console.ReadLine();
            _console.WriteLine("");
            _console.Write("Enter password: ");
            var password = _console.ReadLine();
            _console.Clear();
            var loginSuccess = await _security.Login(user, password);
            if (loginSuccess)
            {
                _console.WriteLine($"Welcome your ID is {_security.Identity.LocalId}!");
                _console.WriteLine(_user.GetUserData(_security.Identity.IdToken));
            }
            else
            {
                _console.WriteLine("Login failed! Press any key to close...");
                _console.ReadLine();
            }
        }
    }
}