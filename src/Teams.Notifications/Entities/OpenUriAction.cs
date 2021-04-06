using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Teams.Notifications.Entities
{
    public record OpenUriAction(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("targets")] IList<OpenUriTarget> Targets
    )
    {
        [JsonPropertyName("@type")]
        public string Type => "OpenUri";
    }
}
