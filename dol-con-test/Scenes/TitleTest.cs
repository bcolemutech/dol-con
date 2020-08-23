using dol_con.Models;
using dol_con.Scenes;
using dol_con.Services;
using dol_con.Utilities;
using NSubstitute;
using Xunit;

namespace dol_con_test.Scenes
{
    public class TitleTest
    {
        private readonly Title _title;
        private readonly IConsoleWrapper _console;
        private readonly ISecurityService _security;
        private readonly IUserService _user;
        public TitleTest()
        {
            _user = Substitute.For<IUserService>();
            _user.GetUserData("TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBtYWxlc3VhZGEgZWxlbWVudHVtIGFudGUgYXQgZmV1Z2lh")
                .Returns(
                    "[{\"date\":\"2020-08-24T20:11:37.2371547+00:00\",\"temperatureC\":-2,\"temperatureF\":29,\"summary\":\"Mild\"}]");
            _console = Substitute.For<IConsoleWrapper>();
            _console.ReadLine().Returns("text");
            _security = Substitute.For<ISecurityService>();
            var id = new Identity
            {
                Kind =  "identitytoolkit#VerifyPasswordResponse",
                LocalId = "TG9yZW0gaXBzdW0=",
                Email = "test@mail.com",
                DisplayName = "",
                IdToken = "TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBtYWxlc3VhZGEgZWxlbWVudHVtIGFudGUgYXQgZmV1Z2lh",
                Registered = true,
                RefreshToken = "dC4gUGhhc2VsbHVzIGxlbyBwdXJ1cywgZmV1Z2lhdCBldSBsYWNpbmlhIGVnZXQsIHJob25jdXMgbm9uIGF1Z3VlLiBQcm9pbiBiaWJlbmR1bSB2dWxwdXRhdGUgZmF1",
                ExpiresIn = 3600
            };
            _security.Identity.Returns(id);
            _security.Login(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            _title = new Title(_console, _security, _user);
        }

        [Fact]
        public void TitleShowsAsciiArt()
        {
            _title.Show();

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
            _title.Show();
            _console.Received(1).Write("Enter email: ");
            _console.Received(1).Write("Enter password: ");
            _console.Received(2).ReadLine();
            _console.Received(1).Clear();
            _security.Received(1).Login("text", "text");
        }

        [Fact]
        public void ShouldDisplayFailureAndPromptIfLoginFailed()
        {
            _security.Login(Arg.Any<string>(), Arg.Any<string>()).Returns(false);
            
            _title.Show();
            
            _console.Received(1).WriteLine("Login failed! Press any key to close...");
        }
        
        [Fact]
        public void ShouldDisplaySuccessAndUserNameIfLoginSucceeded()
        {
            _title.Show();

            _console.Received(1).WriteLine("Welcome your ID is TG9yZW0gaXBzdW0=!");
        }

        [Fact]
        public void ShouldGetAndDisplayJsonUserDataIfLoginSucceeded()
        {
            _title.Show();
            
            _user.Received(1).GetUserData("TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBtYWxlc3VhZGEgZWxlbWVudHVtIGFudGUgYXQgZmV1Z2lh");
            
            _console.Received(1).WriteLine("[{\"date\":\"2020-08-24T20:11:37.2371547+00:00\",\"temperatureC\":-2,\"temperatureF\":29,\"summary\":\"Mild\"}]");
        }
    }
}