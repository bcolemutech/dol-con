using System.Net.Http;

namespace dol_con.Services
{
    public interface IUserService
    {
        string GetUserData(string idToken);
    }

    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _factory;

        public UserService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public string GetUserData(string idToken)
        {
            var client = _factory.CreateClient();
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api-nlx462roma-uc.a.run.app/weatherforecast");
            
            request.Headers.Add("Authorization", "Bearer " + idToken);

            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}