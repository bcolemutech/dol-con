using System;
using dol_con.Utilities;
using dol_sdk.Controllers;
using dol_sdk.Enums;

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
        private readonly IAdminView _adminView;
        public CharacterView(IConsoleWrapper console,
            ICharacterController characterController,
            IMainView mainView,
            INewCharacterView newCharacterView,
            IAdminView adminView)
        {
            _console = console;
            _character = characterController;
            _mainView = mainView;
            _newCharacterView = newCharacterView;
            _adminView = adminView;
        }

        public void Show()
        {
            var player = _character.GetCharacterData();
            _console.Clear();
            _console.WriteLine($"Welcome {_character.User.Email} choose a character to play:");
            var i = 1;
            foreach (var character in player.Characters)
            {
                _console.WriteLine($"{i} - {character.Name}");
                i++;
            }

            _console.WriteLine("N - Create a new character.");
            _console.WriteLine("D # - Delete a character where # is the character ID.");
            if (player.Authority == Authority.Admin)
            {
                _console.WriteLine("A - Admin options.");
            }
            _console.Write("Enter selection: ");
            var retry = true;
            var tries = 0;
            while (retry && tries < 3)
            {
                retry = false;
                var selection = _console.ReadLine(1);
                var split = selection.Split(' ');
                if (int.TryParse(selection, out _))
                {
                    _mainView.Show(player.Characters[Convert.ToInt32(selection) - 1].Name);
                }
                else if (selection.ToUpper() == "N")
                {
                    _newCharacterView.Show();
                }
                else if (selection.ToUpper().StartsWith('D') &&
                         split.Length == 2 &&
                         int.TryParse(split[1], out _) &&
                         player.Characters.Count > 0 &&
                         Convert.ToInt32(split[1]) <= player.Characters.Count)
                {
                    var name = player.Characters[Convert.ToInt32(split[1]) - 1].Name;
                    _console.Write($"Are you sure you want delete {name}? (Y)es or (N)o: ");
                    var yesNo = _console.ReadLine(2);
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
                else if (selection.ToUpper().Trim() == "A")
                {
                    _adminView.Show();
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