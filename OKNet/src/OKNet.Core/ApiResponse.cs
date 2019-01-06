using Newtonsoft.Json;

namespace OKNet.Core
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; }
        public T Data { get; }

        public ApiResponse(int statusCode, string data)
        {
            StatusCode = statusCode;
            Data = JsonConvert.DeserializeObject<T>(data);
        }
    }
}