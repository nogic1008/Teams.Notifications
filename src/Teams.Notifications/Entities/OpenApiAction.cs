using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Teams.Notifications.Entities
{
    public record OpenApiAction(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("targets")] IList<OpenApiTarget> Targets
    )
    {
        [JsonPropertyName("@type")]
        public string Type => "OpenUri";
    }
}
