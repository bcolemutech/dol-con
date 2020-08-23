using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using dol_con.Models;
using dol_con.Services;
using dol_con_test.TestHelpers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace dol_con_test.Services
{
    public class SecurityServiceTests
    {
        private readonly ISecurityService _sut;
        private readonly IHttpClientFactory _clientFactory;

        public SecurityServiceTests()
        {
            _clientFactory = Substitute.For<IHttpClientFactory>();

            var config = Substitute.For<IConfiguration>();

            config["FirebaseUri"].Returns("http://good.uri");
            
            _sut = new SecurityService(_clientFactory, config);
        }

        [Fact]
        public async Task LoginShouldGetAndSaveUserIdentity()
        {
            var id = new Identity
            {
                Kind =  "identitytoolkit#VerifyPasswordResponse",
                LocalId = "TG9yZW0gaXBzdW0=",
                Email = "test@mail.com",
                DisplayName = "",
                IdToken = "TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBtYWxlc3VhZGEgZWxlbWVudHVtIGFudGUgYXQgZmV1Z2lh",
                Registered = true,
                RefreshToken = "dC4gUGhhc2VsbHVzIGxlbyBwdXJ1cywgZmV1Z2lhdCBldSBsYWNpbmlhIGVnZXQsIHJob25jdXMgbm9uIGF1Z3VlLiBQcm9pbiBiaWJlbmR1bSB2dWxwdXRhdGUgZmF1",
                ExpiresIn = 3600
            };
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage() {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json") 
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _clientFactory.CreateClient().Returns(fakeHttpClient);

            var success = await _sut.Login("test", "pass");
            
            success.Should().BeTrue();

            var actual = _sut.Identity;
            
            actual.Should().BeEquivalentTo(id);
        }
        
        [Fact]
        public async Task LoginShouldReturnFalseIfBadRequest()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage() {
                StatusCode = HttpStatusCode.BadRequest
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _clientFactory.CreateClient().Returns(fakeHttpClient);

            var success = await _sut.Login("test", "pass");
            
            success.Should().BeFalse();
        }
    }
}