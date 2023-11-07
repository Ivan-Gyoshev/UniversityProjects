using Course.Project.NASA.Client.Models;

namespace Course.Project.NASA.Client
{
    public interface INasaClient
    {
        Task<ImageOfTheDayResponse> GetImageOfTheDayAsync(string apiKey, DateTimeOffset imageDate);
    }
}
