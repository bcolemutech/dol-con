using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using dol_sdk.Controllers;
using dol_sdk.POCOs;
using dol_sdk.Services;
using dol_sdk_test.TestHelpers;
using Firebase.Auth;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace dol_sdk_test.Controllers
{
    public class CharacterControllerTests
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;

        public CharacterControllerTests()
        {
            _factory = Substitute.For<IHttpClientFactory>();
            var configuration = Substitute.For<IConfiguration>();

            _configuration = configuration;
            _configuration["DolApiUri"].Returns("https://bogus.run.app/");

            var security = Substitute.For<ISecurityService>();
            var provider = Substitute.For<IFirebaseAuthProvider>();
            _securityService = security;
            _securityService.Identity.Returns(new FirebaseAuthLink(provider,
                new FirebaseAuth
                {
                    FirebaseToken = "fakeToken",
                    User = new User
                    {
                        LocalId = "12345"
                    }
                }));
        }

        [Fact]
        public void GetCharacterDataShouldUseTokenToRetrieveListOfCharacters()
        {
            var expected = new List<Character>()
            {
                new Character
                {
                    Name = "Sally"
                },
                new Character
                {
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
            var fakeHttpClient = Substitute.For<HttpClient>(fakeHttpMessageHandler);

            _factory.CreateClient().Returns(fakeHttpClient);
            
            var sut = new CharacterController(_factory, _configuration, _securityService);
            
            var actual = sut.GetCharacterData();

            fakeHttpMessageHandler.RequestMessage.Method.Should().Be(HttpMethod.Get);
            fakeHttpMessageHandler.RequestMessage.RequestUri.Should().Be("https://bogus.run.app/character");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Scheme.Should().Be("Bearer");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Parameter.Should().Be("fakeToken");
            

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void UserLocalIdShouldReturnValueFromSecurityService()
        {
            var sut = new CharacterController(_factory, _configuration, _securityService);
            sut.User.LocalId.Should().Be("12345");
        }

        [Fact]
        public void DeleteShouldSendDeleteRequestToCharacter()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _factory.CreateClient().Returns(fakeHttpClient);
            
            var sut = new CharacterController(_factory, _configuration, _securityService);

            sut.Delete("Bob");

            fakeHttpMessageHandler.RequestMessage.Method.Should().Be(HttpMethod.Delete);
            fakeHttpMessageHandler.RequestMessage.RequestUri.Should().Be("https://bogus.run.app/character/Bob");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Scheme.Should().Be("Bearer");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Parameter.Should().Be("fakeToken");
        }

        [Fact]
        public void CreateCharacterShouldSendPutRequestToCharacterWithName()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _factory.CreateClient().Returns(fakeHttpClient);
            
            var sut = new CharacterController(_factory, _configuration, _securityService);

            sut.CreateCharacter("Jake");

            fakeHttpMessageHandler.RequestMessage.Method.Should().Be(HttpMethod.Put);
            fakeHttpMessageHandler.RequestMessage.RequestUri.Should().Be("https://bogus.run.app/character/Jake");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Scheme.Should().Be("Bearer");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Parameter.Should().Be("fakeToken");
        }
    }
}