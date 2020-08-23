using dol_con.Utilities;

namespace dol_con.Scenes
{
    public interface ITitle
    {
        void Show();
    }

    public class Title : ITitle
    {
        private readonly IConsoleWrapper _console;

        public Title(IConsoleWrapper console)
        {
            _console = console;
        }

        public void Show()
        {
            _console.WriteLine("·▄▄▄▄        ▄▄▌  ");
            _console.WriteLine("██· ██  ▄█▀▄ ██•  ");
            _console.WriteLine("▐█▪ ▐█▌▐█▌.▐▌██ ▪ ");
            _console.WriteLine("██. ██ ▐█▌.▐▌▐█▌ ▄");
            _console.WriteLine("▀▀▀▀▀•  ▀█▄▀▪.▀▀▀ ");
            _console.WriteLine("");
            _console.WriteLine("Login to proceed.");
            _console.Write("Enter email: ");
        }
    }
}