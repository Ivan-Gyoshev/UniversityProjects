using System.Net.Mime;
using System.Text.Json;

namespace Course.Project.NASA.Client
{
    public abstract class HttpClientBase
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions serializerOptions;

        public HttpClientBase(HttpClient client)
        {
            this.client = client;

            client.Timeout = TimeSpan.FromSeconds(45);

            serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = false
            };
        }

        protected async Task<(HttpResponseMessage Response, T Data)> ExecuteRequestAsync<T>(HttpRequestMessage request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            using (HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (var contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        T responseObject = await JsonSerializer.DeserializeAsync<T>(contentStream, serializerOptions).ConfigureAwait(false);
                        return (response, responseObject);
                    }
                }
                else
                {
                    return (response, default);
                }
            }
        }

        protected HttpRequestMessage CreateJsonPostRequest<T>(T model, string resource)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, resource) { Content = new StringContent(JsonSerializer.Serialize(model), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json) };

            return httpRequestMessage;
        }
    }
}
