using System.Net.Http;

namespace NewBoardRestApi.Http
{
    public class HttpClientWrapper
    {
        public readonly string Url;


        public HttpClientWrapper(string url)
        {
            Url = url;
        }


        public HttpClientResponse GetResponse()
        {
            var client = new HttpClient();
            var stream = client.GetStreamAsync(Url);
            stream.Wait();

            return new HttpClientResponse(stream.Result);
        }
    }
}