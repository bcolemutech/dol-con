using System;
using dol_con.Services;
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
            _console = Substitute.For<IConsoleWrapper>();
            _newCharacter = Substitute.For<INewCharacterView>();
            _sut = new UserView(_console, _userController);
        }

        [Fact]
        public void ShowShouldClearAndDisplayUser()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShowShouldListAvailableCharactersAndOptionToCreate()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void EnteringValidCharacterNumberShouldShowMainView()
        {
            throw new NotImplementedException();
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