using dol_con.Utilities;
using dol_con.Views;
using dol_sdk.Controllers;
using dol_sdk.Services;
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
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            
            IFirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig(configuration["FirebaseApiKey"]));
            
            var serviceProvider = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton(auth)
                .AddTransient<IConsoleWrapper, ConsoleWrapper>()
                .AddSingleton<ISecurityService, SecurityService>()
                .AddSingleton<ICharacterController, CharacterController>()
                .AddTransient<ICharacterView, CharacterView>()
                .AddTransient<INewCharacterView, NewCharacterView>()
                .AddTransient<ILoginView, LoginView>()
                .AddTransient<IMainView, MainView>()
                .AddTransient<IMainController, MainController>()
                .AddHttpClient()
                .BuildServiceProvider();
            
            var login = serviceProvider.GetService<ILoginView>();

            var test = args != null && args.Length > 0;

            login.Show(test);
        }
    }
}