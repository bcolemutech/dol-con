﻿using dol_con.Controllers;
using dol_con.Services;
using dol_con.Utilities;

namespace dol_con.Views
{
    public interface ILoginView
    {
        void Show(bool test);
    }

    public class LoginView : ILoginView
    {
        private readonly IConsoleWrapper _console;
        private readonly ISecurityService _security;
        private readonly IUserView _userView;

        public LoginView(IConsoleWrapper console, ISecurityService securityService, IUserView userView)
        {
            _console = console;
            _security = securityService;
            _userView = userView;
        }

        public void Show(bool test = false)
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

            _security.Login(user, password);
            if (_security.Identity?.User == null)
            {
                _console.WriteLine("Login failed! Press any key to close...");
                _console.ReadLine();
            }
            else
            {
                _console.WriteLine($"Welcome your ID is {_security.Identity.User.LocalId}!");
                _userView.Show();
            }
        }
    }
}