using System.Net;
using System.Net.Http;
using dol_sdk.Controllers;
using dol_sdk.Enums;
using dol_sdk.Services;
using dol_sdk_test.TestHelpers;
using Firebase.Auth;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace dol_sdk_test.Controllers
{
    public class AdminControllerTest
    {
        private readonly IAdminController _sut;
        private readonly FakeHttpMessageHandler _fakeHttpMessageHandler;

        public AdminControllerTest()
        {
            var factory = Substitute.For<IHttpClientFactory>();
            var configuration = Substitute.For<IConfiguration>();

            var configuration1 = configuration;
            configuration1["DolApiUri"].Returns("https://bogus.run.app/");

            var security = Substitute.For<ISecurityService>();
            var provider = Substitute.For<IFirebaseAuthProvider>();
            var securityService = security;
            securityService.Identity.Returns(new FirebaseAuthLink(provider,
                new FirebaseAuth
                {
                    FirebaseToken = "fakeToken",
                    User = new User
                    {
                        LocalId = "12345"
                    }
                }));
            _fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            });
            var fakeHttpClient = new HttpClient(_fakeHttpMessageHandler);

            factory.CreateClient().Returns(fakeHttpClient);

            _sut = new AdminController(factory, configuration1, securityService);
        }

        [Fact]
        public void CreateCharacterShouldSendPutRequestToCharacterWithName()
        {
            _sut.UpdateUser("Jake@test.com", Authority.Player);

            _fakeHttpMessageHandler.RequestMessage.Method.Should().Be(HttpMethod.Post);
            _fakeHttpMessageHandler.RequestMessage.RequestUri.Should().Be("https://bogus.run.app/user");
            _fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Scheme.Should().Be("Bearer");
            _fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Parameter.Should().Be("fakeToken");
            _fakeHttpMessageHandler.RequestMessage.Content.ReadAsStringAsync().Result.Should()
                .Be("{\"Email\":\"Jake@test.com\",\"Authority\":2}");
        }
    }
}
