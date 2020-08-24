using dol_con;
using Xunit;

namespace dol_con_test
{
    public class ProgramTest
    {
        [Fact]
        public void CanRunMain()
        {
            Program.Main(new []{"test"});
        }
    }
}