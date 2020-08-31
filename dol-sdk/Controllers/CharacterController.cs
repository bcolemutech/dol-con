﻿using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using dol_sdk.POCOs;
using dol_sdk.Services;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace dol_sdk.Controllers
{
    public interface ICharacterController
    {
        Player GetCharacterData();
        User User { get; }
        void Delete(int id);
        void CreateCharacter(string name);
    }

    public class CharacterController : ICharacterController
    {
        private const string Bearer = "Bearer";
        private readonly ISecurityService _security;
        private readonly HttpClient _client;
        private readonly string _requestUri;
        
        private string IdToken => _security.Identity.FirebaseToken;

        public CharacterController(IHttpClientFactory factory, IConfiguration configuration, ISecurityService security)
        {
            _security = security;
            _client = factory.CreateClient();
            _requestUri = configuration["DolApiUri"] + "character";
        }

        public Player GetCharacterData()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _requestUri);

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);

            var response = _client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStreamAsync().Result;
            
            var serializer = new JsonSerializer();

            var sr = new StreamReader(stream);
            var jsonTextReader = new JsonTextReader(sr);
            return serializer.Deserialize<Player>(jsonTextReader);
        }

        public User User => _security.Identity.User;
        public void Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_requestUri}/{id}");

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);
            
            var response = _client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }

        public void CreateCharacter(string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_requestUri}/{name}");

            request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, IdToken);
            
            var response = _client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}