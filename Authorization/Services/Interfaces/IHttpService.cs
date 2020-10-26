using System.Net.Http;
using System.Threading.Tasks;

namespace Authorization.Services.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, string hostname);
    }
}
