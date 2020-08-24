using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace dol_con.Services
{
    public interface IUserService
    {
        string GetUserData(string idToken);
    }

    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _configuration;

        public UserService(IHttpClientFactory factory, IConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        public string GetUserData(string idToken)
        {
            var client = _factory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, _configuration["DolApiUri"] + "weatherforecast");
            
            request.Headers.Add("Authorization", "Bearer " + idToken);

            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}