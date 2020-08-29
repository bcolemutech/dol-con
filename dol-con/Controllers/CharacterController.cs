using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using dol_con.POCOs;
using dol_con.Services;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace dol_con.Controllers
{
    public interface ICharacterController
    {
        IEnumerable<Character> GetCharacterData();
        User User { get; }
    }

    public class CharacterController : ICharacterController
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _security;

        public CharacterController(IHttpClientFactory factory, IConfiguration configuration, ISecurityService security)
        {
            _factory = factory;
            _configuration = configuration;
            _security = security;
        }

        public IEnumerable<Character> GetCharacterData()
        {
            var client = _factory.CreateClient();

            var idToken = _security.Identity.FirebaseToken;

            var request = new HttpRequestMessage(HttpMethod.Get, _configuration["DolApiUri"] + "character");

            request.Headers.Add("Authorization", "Bearer " + idToken);

            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStreamAsync().Result;
            
            var serializer = new JsonSerializer();

            using var sr = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(sr);
            return serializer.Deserialize<IEnumerable<Character>>(jsonTextReader);
        }

        public User User => _security.Identity.User;
    }
}