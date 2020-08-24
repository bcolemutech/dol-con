﻿using System.Net;
using System.Net.Http;
using System.Text;
using dol_con.Services;
using dol_con_test.TestHelpers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace dol_con_test.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _sut;
        private readonly IHttpClientFactory _factory;

        public UserServiceTests()
        {
            _factory = Substitute.For<IHttpClientFactory>();
            var configuration = Substitute.For<IConfiguration>();

            configuration["DolApiUri"].Returns("https://bogus.run.app/");
            
            _sut = new UserService(_factory, configuration);
        }

        [Fact]
        public void GetUserShouldUseTokenToRetrieveUserData()
        {
            const string expected = "[{\"date\":\"2020-08-24T20:11:37.2371547+00:00\",\"temperatureC\":-2,\"temperatureF\":29,\"summary\":\"Mild\"}]";
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    expected,
                    Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _factory.CreateClient().Returns(fakeHttpClient);
            
            const string token = "lkjhgflskhfglkjshdfg";
            var actual = _sut.GetUserData(token);

            actual.Should().Be(expected);
        }
    }
}