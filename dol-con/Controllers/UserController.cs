using System.Net.Http;
using dol_con.POCOs;
using dol_con.Views;
using Microsoft.Extensions.Configuration;

namespace dol_con.Controllers
{
    public interface IUserController
    {
        string GetPlayerData(string idToken);
        Player GetPlayerData();
    }

    public class UserController : IUserController
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _configuration;

        public UserController(IHttpClientFactory factory, IConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        public string GetPlayerData(string idToken)
        {
            var client = _factory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, _configuration["DolApiUri"] + "weatherforecast");
            
            request.Headers.Add("Authorization", "Bearer " + idToken);

            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }

        public Player GetPlayerData()
        {
            throw new System.NotImplementedException();
        }
    }
}