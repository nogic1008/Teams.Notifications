using System.Text.Json.Serialization;

namespace Teams.Notifications.Entities
{
    public record OpenApiTarget(
        [property: JsonPropertyName("os")] string OS,
        [property: JsonPropertyName("uri")] string Uri
    )
    {
        public OpenApiTarget(string uri) : this("default", uri) { }
    }
}
