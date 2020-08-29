using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace dol_con_test.TestHelpers
{
    public class FakeHttpMessageHandler : DelegatingHandler
    {
        private readonly HttpResponseMessage _fakeResponse;

        public HttpRequestMessage RequestMessage { get; internal set; }

        public FakeHttpMessageHandler(HttpResponseMessage responseMessage)
        {
            _fakeResponse = responseMessage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestMessage = request;
            return await Task.FromResult(_fakeResponse);
        }
    }
}