using System.Text.Json.Serialization;

namespace Teams.Notifications.Entities
{
    public record MessageFact(
        [property: JsonPropertyName("name")] string? Name,
        [property: JsonPropertyName("value")] string? Value
    );
}
