using Course.Project.NASA.Client.Models;

namespace Course.Project.NASA.Client;

public class NasaClient : HttpClientBase, INasaClient
{
    public NasaClient(HttpClient client) : base(client) { }

    public async Task<ImageOfTheDayResponse> GetImageOfTheDayAsync(string apiKey, DateTimeOffset imageDate)
    {
        string resource = $"planetary/apod?api_key={apiKey}&date={imageDate:yyyy-MM-dd}";

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, resource);

        var result = await ExecuteRequestAsync<ImageOfTheDayResponse>(request).ConfigureAwait(false);

        return result.Data;
    }
}
