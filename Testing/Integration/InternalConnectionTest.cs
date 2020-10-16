using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;
using Xunit;

namespace Testing.Integration
{
    public class WebBuilder : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public WebBuilder(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("http://www.example.com")]
        public async Task TestAccessToInternalAndExternalNetworks(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }
    }
}
