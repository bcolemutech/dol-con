using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using dol_con.Controllers;
using dol_con.POCOs;
using dol_con.Services;
using dol_con_test.TestHelpers;
using Firebase.Auth;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace dol_con_test.Controllers
{
    public class CharacterControllerTests
    {
        private readonly ICharacterController _sut;
        private readonly IHttpClientFactory _factory;
        private readonly ISecurityService _security;

        public CharacterControllerTests()
        {
            _factory = Substitute.For<IHttpClientFactory>();
            var configuration = Substitute.For<IConfiguration>();

            configuration["DolApiUri"].Returns("https://bogus.run.app/");

            _security = Substitute.For<ISecurityService>();
            var provider = Substitute.For<IFirebaseAuthProvider>();
            _security.Identity.Returns(new FirebaseAuthLink(provider,
                new FirebaseAuth {FirebaseToken = "dfghlksjhdfglkjh"}));
            
            _sut = new CharacterController(_factory, configuration, _security);
        }

        [Fact]
        public void GetCharacterDataShouldUseTokenToRetrieveListOfCharacters()
        {
            var expected = new List<Character>()
            {
                new Character
                {
                    Id = 5,
                    Name = "Sally"
                },
                new Character
                {
                    Id = 3,
                    Name = "Bert"
                }
            };

            var expectedJson = JsonConvert.SerializeObject(expected);
            
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    expectedJson,
                    Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _factory.CreateClient().Returns(fakeHttpClient);
            
            var actual = _sut.GetCharacterData();

            actual.Should().BeEquivalentTo(expected);
        }
    }
}