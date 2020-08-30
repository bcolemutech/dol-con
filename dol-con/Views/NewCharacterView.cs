using dol_con.Utilities;
using dol_sdk.Controllers;

namespace dol_con.Views
{
    public interface INewCharacterView
    {
        void Show();
    }

    public class NewCharacterView : INewCharacterView
    {
        private readonly IConsoleWrapper _console;
        private readonly ICharacterController _controller;
        public NewCharacterView(IConsoleWrapper console, ICharacterController controller)
        {
            _console = console;
            _controller = controller;
        }

        public void Show()
        {
            _console.Write("Enter new characters name: ");
            var name = _console.ReadLine(1);
            _console.Write($"Create new character {name}? (Y)es or (N)o: ");
            var response = _console.ReadLine(2);
            if (response.ToUpper().Trim() == "Y")
            {
                _controller.CreateCharacter(name);
            }
        }
    }
}