using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using dol_con.Models;
using Microsoft.Extensions.Configuration;

namespace dol_con.Services
{
    public interface ISecurityService
    {
        Task<bool> Login(string user, string password);
        Identity Identity { get; }
    }

    public class SecurityService : ISecurityService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _configuration;
        public SecurityService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<bool> Login(string user, string password)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", user),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("returnSecureToken", "true"),
            });

            var uri = new Uri(_configuration["FirebaseUri"]);

            using var httpclient = _httpFactory.CreateClient();
            using var response = await httpclient.PostAsync(uri, formContent);
            if (response.StatusCode != HttpStatusCode.OK) return false;
            var id = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Identity = JsonSerializer.Deserialize<Identity>(id, options);
            return true;
        }

        public Identity Identity { get; private set; }
    }
}