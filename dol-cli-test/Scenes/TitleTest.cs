using Xunit;
using dol_cli;
using dol_cli.Utilities;
using NSubstitute;

namespace dol_cli_test.Scenes
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

        [Fact]
        public void TitleShowsOptions()
        {
            
        }
    }
}