using System;
using System.Net.Http;
using System.Threading.Tasks;
using Authorization.Services.Interfaces;

namespace Authorization.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, string hostname)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(hostname);
            return await client.SendAsync(request);
        }
    }
}
