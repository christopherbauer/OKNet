using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace OKNet.Core
{
    public class ApiRequestService
    {
        public ApiResponse<T> MakeRequestWithBasicAuth<T>(Uri baseAddress, string username, string password,
            string uriQuery)
        {
            var authBase = $"{username}:{password}";
            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(authBase));

            var basicAuth = $"Basic {encoded}";

            var client = new HttpClient {BaseAddress = baseAddress};
            client.DefaultRequestHeaders.Add("Authorization", basicAuth);
            var requestUri = uriQuery;
            var response = client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                return new ApiResponse<T>((int) response.StatusCode, data);
            }

            return new ApiResponse<T>((int) response.StatusCode, response.ReasonPhrase);
        }
        public StreamApiResponse MakeStreamRequestWithBasicAuth(Uri baseAddress, string username, string password, string uriQuery)
        {
            var authBase = $"{username}:{password}";
            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(authBase));

            var basicAuth = $"Basic {encoded}";

            var client = new HttpClient {BaseAddress = baseAddress};
            client.DefaultRequestHeaders.Add("Authorization", basicAuth);
            var response = client.GetAsync(uriQuery).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStreamAsync().Result;
                return new StreamApiResponse((int) response.StatusCode, data);
            }

            return new StreamApiResponse((int) response.StatusCode, Stream.Null);
        }
    }
}
