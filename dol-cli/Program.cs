using System;
using dol_cli.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace dol_cli
{
    class Program
    {
        static void Main(string[] args)
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