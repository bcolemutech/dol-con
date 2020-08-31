using dol_con.Utilities;
using dol_con.Views;
using dol_sdk.Controllers;
using dol_sdk.Enums;
using NSubstitute;
using Xunit;

namespace dol_con_test.Views
{
    public class AdminViewTest
    {
        private readonly IAdminView _sut;
        private readonly IConsoleWrapper _console;
        private readonly IAdminController _controller;

        public AdminViewTest()
        {
            _controller = Substitute.For<IAdminController>();
            _console = Substitute.For<IConsoleWrapper>();

            _console.ReadLine(1).Returns("bob@test.com");
            
            _sut = new AdminView(_console, _controller);
        }

        [Fact]
        public void ShowShouldAskForUserEmailFollowedByDesiredAuthority()
        {
            _sut.Show();
                
            _console.Received(1).Clear();
            _console.Received(1).Write("Enter users email: ");
            _console.Received(1).ReadLine(1);
            _console.Received(1).Write("Enter authority. (A)dministrator, (T)ester, or (P)layer: ");
            _console.Received(1).ReadLine(2);
        }

        [Fact]
        public void GivenAdminSelectedThenSendPlayerUpdateToAdmin()
        {
            _console.ReadLine(2).Returns("a");
            
            _sut.Show();

            _controller.Received(1).UpdateUser("bob@test.com", Authority.Admin);
        }
        
        [Fact]
        public void GivenTesterSelectedThenSendPlayerUpdateToTester()
        {
            _console.ReadLine(2).Returns("t");
            
            _sut.Show();

            _controller.Received(1).UpdateUser("bob@test.com", Authority.Tester);
        }
        
        [Fact]
        public void GivenPlayerSelectedThenSendPlayerUpdateToPlayer()
        {
            _console.ReadLine(2).Returns("p");
            
            _sut.Show();

            _controller.Received(1).UpdateUser("bob@test.com", Authority.Player);
        }
        
        [Fact]
        public void GivenInvalidSelectionThenSendPlayerUpdateToPlayer()
        {
            _console.ReadLine(2).Returns("#");
            
            _sut.Show();

            _controller.Received(1).UpdateUser("bob@test.com", Authority.Player);
        }
    }
}
