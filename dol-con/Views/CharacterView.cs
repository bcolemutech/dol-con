﻿using System;
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
            _console.WriteLine("D # - Delete a character where # is the character ID.");
            _console.Write("Enter selection: ");
            var retry = true;
            var tries = 0;
            while (retry && tries < 3)
            {
                retry = false;
                var selection = _console.ReadLine(1);
                var split = selection.Split(' ');
                if (int.TryParse(selection, out _) && characters.Any(x => x.Id == Convert.ToInt32(selection)))
                {
                    _mainView.Show(Convert.ToInt32(selection));
                }
                else if (selection.ToUpper() == "N")
                {
                    _newCharacterView.Show();
                }
                else if (selection.ToUpper().StartsWith('D') &&
                         split.Length == 2 &&
                         int.TryParse(split[1], out _) &&
                         characters.Any(x => x.Id == Convert.ToInt32(split[1])))
                {
                    var id = Convert.ToInt32(split[1]);
                    var name = characters.First(x => x.Id == id).Name;
                    _console.Write($"Are you sure you want delete {name}? (Y)es or (N)o: ");
                    var yesNo = _console.ReadLine(2);
                    if (yesNo.ToUpper().Trim() == "Y")
                    {
                        _character.Delete(id);
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