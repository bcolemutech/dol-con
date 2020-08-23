using Xunit;

namespace dol_cli_test
{
    public class ProgramTest
    {
        [Fact]
        public void CanRunMain()
        {
            dol_cli.Program.Main();
        }
    }
}