using dol_con.Scenes;
using dol_con.Services;
using dol_con.Utilities;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dol_con
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            
            IFirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig(configuration["FirebaseApiKey"]));
            
            var serviceProvider = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton(auth)
                .AddSingleton<ITitle, Title>()
                .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
                .AddSingleton<ISecurityService, SecurityService>()
                .AddSingleton<IUserService, UserService>()
                .AddHttpClient()
                .BuildServiceProvider();
            
            var title = serviceProvider.GetService<ITitle>();

            var test = args != null && args.Length > 0;

            title.Show(test);
        }
    }
}