using System.Text.Json.Serialization;

namespace Teams.Notifications.Entities
{
    public class MessageFact
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
