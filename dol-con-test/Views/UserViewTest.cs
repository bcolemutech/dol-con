using System;
using dol_con.Controllers;
using dol_con.POCOs;
using dol_con.Utilities;
using dol_con.Views;
using NSubstitute;
using Xunit;

namespace dol_con_test.Views
{
    public class UserViewTest
    {
        private readonly IUserView _sut;
        private readonly IUserController _userController;
        private readonly IConsoleWrapper _console;
        private readonly INewCharacterView _newCharacter;

        public UserViewTest()
        {
            _userController = Substitute.For<IUserController>();

            var player = new Player();

            _userController.GetPlayerData().Returns(player);
            
            _console = Substitute.For<IConsoleWrapper>();
            _newCharacter = Substitute.For<INewCharacterView>();
            _sut = new UserView(_console, _userController);
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
            
            _console.Received(1).WriteLine("1 - Sally ");
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
            
        }

        [Fact]
        public void EnteringAnInvalidCharacterNumberShouldPromptAndAskForNewInput()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void EnteringTheLetterNShouldShowNewCharacterView()
        {
            throw new NotImplementedException();
        }
    }
}