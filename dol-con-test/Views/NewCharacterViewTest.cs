using dol_con.Utilities;
using dol_con.Views;
using dol_sdk.Controllers;
using NSubstitute;
using Xunit;

namespace dol_con_test.Views
{
    public class NewCharacterViewTest
    {
        private readonly INewCharacterView _sut;
        private readonly ICharacterController _characterController;
        private readonly IConsoleWrapper _console;

        public NewCharacterViewTest()
        {
            _console = Substitute.For<IConsoleWrapper>();
            _characterController = Substitute.For<ICharacterController>();
            _sut = new NewCharacterView(_console, _characterController);
        }

        [Fact]
        public void ShowShouldAskForCharactersNameAndVerifyChoiceWhenYesThenCreateCharacterThenReturnToCharacterScreen()
        {
            _console.ReadLine(1).Returns("Jake");
            _console.ReadLine(2).Returns("y");
            
            _sut.Show();
            
            _console.Received(1).Write("Enter new characters name: ");
            _console.Received(1).ReadLine(1);
            _console.Received(1).Write("Create new character Jake? (Y)es or (N)o: ");
            _console.Received(1).ReadLine(2);
            _characterController.Received(1).CreateCharacter("Jake");
        }
        
        [Fact]
        public void ShowShouldAskForCharactersNameAndVerifyChoiceWhenNoThenReturnToCharacterScreen()
        {
            _console.ReadLine(1).Returns("Jake");
            _console.ReadLine(2).Returns("n");
            
            _sut.Show();
            
            _console.Received(1).Write("Enter new characters name: ");
            _console.Received(1).ReadLine(1);
            _console.Received(1).Write("Create new character Jake? (Y)es or (N)o: ");
            _console.Received(1).ReadLine(2);
            _characterController.Received(0).CreateCharacter("Jake");
        }
    }
}
