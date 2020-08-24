using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;

namespace dol_con.Services
{
    public interface ISecurityService
    {
        void Login(string user, string password);
        FirebaseAuthLink Identity { get; }
    }

    public class SecurityService : ISecurityService
    {
        private readonly IFirebaseAuthProvider _authProvider;
        public SecurityService(IFirebaseAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public void Login(string user, string password)
        {
            Identity = _authProvider.SignInWithEmailAndPasswordAsync(user, password).Result;
        }

        public FirebaseAuthLink Identity { get; private set; }
    }
}