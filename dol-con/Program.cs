using dol_con.Scenes;
using dol_con.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace dol_con
{
    public static class Program
    {
        public static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITitle, Title>()
                .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
                .BuildServiceProvider();
            
            var title = serviceProvider.GetService<ITitle>();

            title.Show();
        }
    }
}