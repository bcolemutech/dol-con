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
        private readonly ICharacterController _sut;
        private readonly IHttpClientFactory _factory;

        public CharacterControllerTests()
        {
            _factory = Substitute.For<IHttpClientFactory>();
            var configuration = Substitute.For<IConfiguration>();

            configuration["DolApiUri"].Returns("https://bogus.run.app/");

            var security = Substitute.For<ISecurityService>();
            var provider = Substitute.For<IFirebaseAuthProvider>();
            security.Identity.Returns(new FirebaseAuthLink(provider,
                new FirebaseAuth
                {
                    FirebaseToken = "dfghlksjhdfglkjh",
                    User = new User
                    {
                        LocalId = "12345"
                    }
                }));

            _sut = new CharacterController(_factory, configuration, security);
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
            var fakeHttpClient = Substitute.For<HttpClient>(fakeHttpMessageHandler);

            _factory.CreateClient().Returns(fakeHttpClient);
            
            var actual = _sut.GetCharacterData();

            fakeHttpMessageHandler.RequestMessage.RequestUri.Should().Be("https://bogus.run.app/character");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Scheme.Should().Be("Bearer");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Parameter.Should().Be("dfghlksjhdfglkjh");

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void UserLocalIdShouldReturnValueFromSecurityService()
        {
            _sut.User.LocalId.Should().Be("12345");
        }
        
    }
}