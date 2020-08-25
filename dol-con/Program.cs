using dol_con.Controllers;
using dol_con.Services;
using dol_con.Utilities;
using dol_con.Views;
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
                .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
                .AddSingleton<ISecurityService, SecurityService>()
                .AddSingleton<IUserController, UserController>()
                .AddSingleton<IUserView, UserView>()
                .AddSingleton<INewCharacterView, NewCharacterView>()
                .AddSingleton<INewCharacterController, NewCharacterController>()
                .AddSingleton<ILoginView, LoginView>()
                .AddHttpClient()
                .BuildServiceProvider();
            
            var login = serviceProvider.GetService<ILoginView>();

            var test = args != null || args.Length > 0;

            login.Show(test);
        }
    }
}