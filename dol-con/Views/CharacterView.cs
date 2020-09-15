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
        public CharacterView(IConsoleWrapper console,
            ICharacterController characterController,
            IMainView mainView,
            INewCharacterView newCharacterView)
        {
            _console = console;
            _character = characterController;
            _mainView = mainView;
            _newCharacterView = newCharacterView;
        }

        public void Show()
        {
            var characters = _character.GetCharacterData().ToList();
            _console.Clear();
            _console.WriteLine($"Welcome {_character.User.Email} choose a character to play:");
            var i = 1;
            foreach (var character in characters)
            {
                _console.WriteLine($"{i} - {character.Name}");
                i++;
            }

            _console.WriteLine("N - Create a new character.");
            _console.WriteLine("D # - Delete a character where # is the character ID.");
            _console.WriteLine("E - Exit game.");
            _console.Write("Enter selection: ");
            var retry = true;
            var tries = 0;
            while (retry && tries < 3)
            {
                retry = false;
                var selection = _console.ReadLine();
                var split = selection.Split(' ');
                if (int.TryParse(selection, out _))
                {
                    _mainView.Show(characters[Convert.ToInt32(selection) - 1].Name);
                }
                else if (selection.ToUpper() == "N")
                {
                    _newCharacterView.Show();
                    Show();
                }
                else if (selection.ToUpper().StartsWith('D') &&
                         split.Length == 2 &&
                         int.TryParse(split[1], out _) &&
                         characters.Count > 0 &&
                         Convert.ToInt32(split[1]) <= characters.Count)
                {
                    var name = characters[Convert.ToInt32(split[1]) - 1].Name;
                    _console.Write($"Are you sure you want delete {name}? (Y)es or (N)o: ");
                    var yesNo = _console.ReadLine();
                    if (yesNo.ToUpper().Trim() == "Y")
                    {
                        _character.Delete(name);
                        _console.WriteLine($"{name} was deleted");
                        Show();
                    }
                    else
                    {
                        retry = true;
                        tries = 0;
                        _console.Write("Enter selection: ");
                    }
                }
                else if (selection.ToUpper().StartsWith('E'))
                {
                    _console.Write("Are you sure you want to leave? (Y)es or (N)o: ");
                    var yesNo = _console.ReadLine();
                    if (yesNo.ToUpper().Trim() == "Y")
                    {
                        break;
                    }

                    retry = true;
                    tries = 0;
                    _console.Write("Enter selection: ");
                }
                else
                {
                    tries++;
                    var returnMessage = tries < 3 ? "Try again." : "Go home...";
                    _console.WriteLine($"Invalid selection. {returnMessage}");
                    retry = true;
                }
            }
        }
    }
}