using System.Net.Http;

namespace Authorization.Services.Interfaces
{
    public interface IFacebookApiService : IHttpService
    {
        HttpRequestMessage PrepareProfileFetchRequest(HttpMethod method, string path, string token);
    }
}
