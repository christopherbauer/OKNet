using System.IO;

namespace OKNet.Core
{
    public class StreamApiResponse
    {
        public int StatusCode { get; }
        public Stream Data { get; }

        public StreamApiResponse(int statusCode, Stream data)
        {
            StatusCode = statusCode;
            Data = data;
        }
    }
}