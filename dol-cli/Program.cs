using dol_cli.Scenes;
using dol_cli.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace dol_cli
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