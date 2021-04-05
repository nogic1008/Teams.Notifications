using Newtonsoft.Json;
using System.Collections.Generic;

namespace Teams.Notifications
{
    public class MessageCard
    {
        [JsonProperty("@type")]
        public string Type { get; } = "MessageCard";

        [JsonProperty("@context")]
        public string Context { get; } = "http://schema.org/extensions";

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("themeColor")]
        public string Color { get; set; }

        [JsonProperty("sections")]
        public IList<MessageSection> Sections { get; set; }

        [JsonProperty("potentialAction")]
        public IList<PotentialAction> PotentialActions { get; set; }
    }
}
