﻿using dol_con.Utilities;
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
            _console.ReadLine().Returns("Jake");
            _console.ReadLine().Returns("y");
            
            _sut.Show();
            
            _console.Received(1).Write("Enter new characters name: ");
            _console.Received(1).ReadLine();
            _console.Received(1).Write("Create new character Jake? (Y)es or (N)o: ");
            _console.Received(1).ReadLine();
            _characterController.Received(1).CreateCharacter("Jake");
        }
        
        [Fact]
        public void ShowShouldAskForCharactersNameAndVerifyChoiceWhenNoThenReturnToCharacterScreen()
        {
            _console.ReadLine().Returns("Jake");
            _console.ReadLine().Returns("n");
            
            _sut.Show();
            
            _console.Received(1).Write("Enter new characters name: ");
            _console.Received(1).ReadLine();
            _console.Received(1).Write("Create new character Jake? (Y)es or (N)o: ");
            _console.Received(1).ReadLine();
            _characterController.Received(0).CreateCharacter("Jake");
        }
    }
}
