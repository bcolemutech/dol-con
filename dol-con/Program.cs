using dol_con.Scenes;
using dol_con.Services;
using dol_con.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dol_con
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITitle, Title>()
                .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
                .AddSingleton<ISecurityService, SecurityService>()
                .AddSingleton<IUserService, UserService>()
                .AddHttpClient()
                .AddSingleton(configuration)
                .BuildServiceProvider();
            
            var title = serviceProvider.GetService<ITitle>();

            var test = args != null && args.Length > 0;

            title.Show(test);
        }
    }
}