using dol_con.Scenes;
using dol_con.Utilities;
using NSubstitute;
using Xunit;

namespace dol_con_test.Scenes
{
    public class TitleTest
    {
        private readonly Title _title;
        private readonly IConsoleWrapper _console; 
        public TitleTest()
        {
            _console = Substitute.For<IConsoleWrapper>();
            _title = new Title(_console);
        }

        [Fact]
        public void TitleShowsAsciiArt()
        {
            _title.Show();

            _console.Received(1).WriteLine("·▄▄▄▄        ▄▄▌  ");
            _console.Received(1).WriteLine("██· ██  ▄█▀▄ ██•  ");
            _console.Received(1).WriteLine("▐█▪ ▐█▌▐█▌.▐▌██ ▪ ");
            _console.Received(1).WriteLine("██. ██ ▐█▌.▐▌▐█▌ ▄");
            _console.Received(1).WriteLine("▀▀▀▀▀•  ▀█▄▀▪.▀▀▀ ");
            _console.Received(1).WriteLine("");
            _console.Received(1).WriteLine("Login to proceed.");
            _console.Received(1).Write("Enter email: ");
        }
    }
}