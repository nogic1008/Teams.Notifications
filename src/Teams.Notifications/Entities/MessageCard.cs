using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Teams.Notifications.Entities
{
    public record MessageCard(
        [property: JsonPropertyName("title")] string? Title,
        [property: JsonPropertyName("text")] string? Text,
        [property: JsonPropertyName("themeColor")] string? ThemeColor,
        [property: JsonPropertyName("sections")] IList<MessageSection>? Sections,
        [property: JsonPropertyName("potentialAction")] IList<OpenApiAction>? PotentialActions
    )
    {
        [JsonPropertyName("@type")]
        public string Type => "MessageCard";

        [JsonPropertyName("@context")]
        public string Context => "http://schema.org/extensions";
    }
}
