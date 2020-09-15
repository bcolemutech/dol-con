using dol_sdk.Services;
using dol_con.Utilities;
using dol_con.Views;
using Firebase.Auth;
using NSubstitute;
using Xunit;

namespace dol_con_test.Views
{
    public class LoginViewTest
    {
        private readonly LoginView _loginView;
        private readonly IConsoleWrapper _console;
        private readonly ISecurityService _security;
        private readonly ICharacterView _characterView;
        
        public LoginViewTest()
        {
            _characterView = Substitute.For<ICharacterView>();
            _console = Substitute.For<IConsoleWrapper>();
            var step = 0;
            var responses = new[] {"user", "pass", " "};
            _console.ReadLine().Returns(x =>
            {
                var response = responses[step];
                step++;
                return response;
            });
            _security = Substitute.For<ISecurityService>();
            var authProvider = Substitute.For<IFirebaseAuthProvider>();
            var id = new FirebaseAuthLink(authProvider, new FirebaseAuth())
            {
                FirebaseToken =
                    "TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBtYWxlc3VhZGEgZWxlbWVudHVtIGFudGUgYXQgZmV1Z2lh",
                User = new User {Email = "test@mail.com", LocalId = "TG9yZW0gaXBzdW0="}
            };

            _security.Identity.Returns(id);
            _loginView = new LoginView(_console, _security, _characterView);
        }

        [Fact]
        public void TitleShowsAsciiArt()
        {
            _loginView.Show();

            _console.Received(1).WriteLine("·▄▄▄▄        ▄▄▌  ");
            _console.Received(1).WriteLine("██· ██  ▄█▀▄ ██•  ");
            _console.Received(1).WriteLine("▐█▪ ▐█▌▐█▌.▐▌██ ▪ ");
            _console.Received(1).WriteLine("██. ██ ▐█▌.▐▌▐█▌ ▄");
            _console.Received(1).WriteLine("▀▀▀▀▀•  ▀█▄▀▪.▀▀▀ ");

            _console.Received(1).WriteLine("Login to proceed.");
        }

        [Fact]
        public void ShowShouldRequireLogin()
        {
            _loginView.Show();
            _console.Received(1).Write("Enter email: ");
            _console.Received(1).Write("Enter password: ");
            _console.Received(2).ReadLine();
            _security.Received(1).Login("user", "pass");
        }

        [Fact]
        public void ShouldDisplayFailureAndPromptIfLoginFailed()
        {
            var authProvider = Substitute.For<IFirebaseAuthProvider>();
            var id = new FirebaseAuthLink(authProvider, new FirebaseAuth());

            _security.Identity.Returns(id);
            
            _loginView.Show();
            
            _console.Received(1).WriteLine("Login failed! Press any key to close...");
        }
        
        [Fact]
        public void ShouldDisplaySuccessAndUserNameIfLoginSucceeded()
        {
            _loginView.Show();

            _console.Received(1).WriteLine("Welcome your ID is TG9yZW0gaXBzdW0=!");
        }

        [Fact]
        public void ShouldShowMainIfLoginSucceeded()
        {
            _loginView.Show();

            _characterView.Received(1).Show();
        }
    }
}