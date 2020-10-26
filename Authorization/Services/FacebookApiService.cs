using System.Net.Http;
using Authorization.Services.Interfaces;

namespace Authorization.Services
{
    public class FacebookApiService : HttpService, IFacebookApiService
    {
        public FacebookApiService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public HttpRequestMessage PrepareProfileFetchRequest(HttpMethod method, string path, string token)
        {
            path += "fields=id,name,email" + "&access_token=" + token;
            var request = new HttpRequestMessage(method, path);
            return request;
        }
    }
}
