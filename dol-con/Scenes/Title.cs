using System;
using System.Threading.Tasks;
using dol_con.Services;
using dol_con.Utilities;

namespace dol_con.Scenes
{
    public interface ITitle
    {
        void Show(bool test);
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

        public void Show(bool test = false)
        {
            try
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
                //_console.Clear();
                _security.Login(user, password);
                if (_security.Identity?.User == null)
                {
                    _console.WriteLine("Login failed! Press any key to close...");
                    _console.ReadLine();

                }
                else
                {
                    _console.WriteLine($"Welcome your ID is {_security.Identity.User.LocalId}!");
                    _console.WriteLine(_user.GetUserData(_security.Identity.FirebaseToken));
                }
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                _console.WriteLine(e.StackTrace);
                throw;
            }
            finally
            {
                Task.Delay(10000);
            }
            
        }
    }
}