using System.Text.Json.Serialization;

namespace Teams.Notifications.Entities
{
    public record OpenUriTarget(
        [property: JsonPropertyName("os")] string OS,
        [property: JsonPropertyName("uri")] string Uri
    )
    {
        public OpenUriTarget(string uri) : this("default", uri) { }
    }
}
