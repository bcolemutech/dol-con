using dol_sdk.Controllers;
using dol_sdk.POCOs;
using dol_con.Utilities;
using dol_con.Views;
using Firebase.Auth;
using NSubstitute;
using Xunit;

namespace dol_con_test.Views
{
    public class CharacterViewTest
    {
        private readonly ICharacterView _sut;
        private readonly ICharacterController _characterController;
        private readonly IConsoleWrapper _console;
        private readonly INewCharacterView _newCharacter;
        private readonly IMainView _mainView;

        public CharacterViewTest()
        {
            _characterController = Substitute.For<ICharacterController>();

            var characters = new[]
            {
                new Character {Name = "Sally", Id = 1}, new Character {Name = "Rick", Id = 2},
                new Character {Name = "Joe", Id = 3}
            };

            _characterController.GetCharacterData().Returns(characters);
            var user = new User
            { 
                Email = "bob@test.com"
            };

            _characterController.User.Returns(user);
            
            _console = Substitute.For<IConsoleWrapper>();
            _newCharacter = Substitute.For<INewCharacterView>();
            _mainView = Substitute.For<IMainView>();
            _sut = new CharacterView(_console, _characterController, _mainView, _newCharacter);
        }

        [Fact]
        public void ShowShouldClearAndDisplayUser()
        {
            _sut.Show();
            
            _console.Received(1).Clear();
            
            _console.Received(1).WriteLine("Welcome bob@test.com choose a character to play:");

        }

        [Fact]
        public void ShowShouldListAvailableCharactersAndOptionToCreate()
        {
            _sut.Show();

            _characterController.Received(1).GetCharacterData();
            
            _console.Received(1).WriteLine("1 - Sally");
            _console.Received(1).WriteLine("2 - Rick");
            _console.Received(1).WriteLine("3 - Joe");
            _console.Received(1).WriteLine("N - Create a new character.");
            _console.Received(1).WriteLine("D - Delete a character.");
            _console.Received(1).Write("Enter selection: ");
        }

        [Fact]
        public void EnteringValidCharacterNumberShouldShowMainView()
        {
            _console.ReadLine().Returns("1");
            
            _sut.Show();

            _mainView.Received(1).Show(1);
        }

        [Fact]
        public void EnteringAnInvalidCharacterNumberShouldPromptAndAskForNewInput()
        {
            _console.ReadLine().Returns("g");
            _console.When(x => x.ReadLine()).Do(x => _console.Clear());
            
            _sut.Show();
            
            _console.Received(1).WriteLine("1 - Sally");
            _console.Received(1).WriteLine("2 - Rick");
            _console.Received(1).WriteLine("3 - Joe");
            _console.Received(1).WriteLine("N - Create a new character.");
            _console.Received(1).WriteLine("D - Delete a character.");
            _console.Received(1).Write("Enter selection: ");
            _console.Received(3).WriteLine("Invalid selection. Try again.");
            _console.Received(1).WriteLine("Invalid selection. Go home...");
                
        }

        [Fact]
        public void EnteringTheLetterNShouldShowNewCharacterView()
        {
            _console.ReadLine().Returns("n");

            _sut.Show();

            _newCharacter.Received(1).Show();
        }
    }
}