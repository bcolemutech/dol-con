using System;
using System.Net.Http;
using System.Net.Http.Headers;
using dol_sdk.Enums;
using dol_sdk.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace dol_sdk.Controllers
{
    public interface IAdminController
    {
        void UpdateUser(string email, Authority authority);
    }

    public class AdminController : IAdminController
    {
        private const string Bearer = "Bearer";
        private readonly ISecurityService _security;
        private readonly HttpClient _client;
        private readonly string _requestUri;
        public AdminController(IHttpClientFactory factory, IConfiguration configuration, ISecurityService security)
        {
            _security = security;
            _client = factory.CreateClient();
            _requestUri = configuration["DolApiUri"] + "user";
        }
        
        private string IdToken => _security.Identity.FirebaseToken;

        public void UpdateUser(string email, Authority authority)
        {
            var content = new PlayerRequest(email, authority);

            var contentJson = JsonConvert.SerializeObject(content); 

            var request = new HttpRequestMessage(HttpMethod.Post, _requestUri);

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);
            request.Content = new StringContent(contentJson);
            
            var response = _client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }
    }

    public readonly struct PlayerRequest
    {
        public PlayerRequest(string email, Authority authority)
        {
            Email = email;
            Authority = authority;
        }

        public string Email { get; }
        public Authority Authority { get; }
    }
}
