using System.Text.Json.Serialization;

namespace Course.Project.NASA.Client.Models
{
    public class ImageOfTheDayResponse
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("explanation")]
        public string Explanation { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }
    }
}
