using System;
using System.Linq;
using dol_con.Utilities;
using dol_sdk.Controllers;

namespace dol_con.Views
{
    public interface ICharacterView
    {
        void Show();
    }

    public class CharacterView : ICharacterView
    {
        private readonly IConsoleWrapper _console;
        private readonly ICharacterController _character;
        private readonly IMainView _mainView;
        private readonly INewCharacterView _newCharacterView;
        public CharacterView(IConsoleWrapper console, ICharacterController characterController, IMainView mainView, INewCharacterView newCharacterView)
        {
            _console = console;
            _character = characterController;
            _mainView = mainView;
            _newCharacterView = newCharacterView;
        }

        public void Show()
        {
            var characters = _character.GetCharacterData().ToArray();
            _console.Clear();
            _console.WriteLine($"Welcome {_character.User.Email} choose a character to play:");
            foreach (var character in characters)
            {
                _console.WriteLine($"{character.Id} - {character.Name}");
            }

            _console.WriteLine("N - Create a new character.");
            _console.WriteLine("D - Delete a character.");
            _console.Write("Enter selection: ");
            var badChoice = true;
            var tries = 0;
            while (badChoice && tries < 3)
            {
                var selection = _console.ReadLine();
                if (int.TryParse(selection, out _) && characters.Any(x => x.Id == Convert.ToInt32(selection)))
                {
                    badChoice = false;
                    _mainView.Show(Convert.ToInt32(selection));
                }
                else if (selection.ToUpper() == "N")
                {
                    badChoice = false;
                    _newCharacterView.Show();
                }
                else
                {
                    _console.WriteLine("Invalid selection. Try again.");
                    tries++;
                }
            }

            if (badChoice)
            {
                _console.WriteLine("Invalid selection. Go home...");
            }
        }
    }
}