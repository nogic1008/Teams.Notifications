using System.Text.Json;

namespace Teams.Notifications.Tests
{
    public static class JsonConfig
    {
        public static JsonSerializerOptions Default = new(JsonSerializerDefaults.Web)
        {
            WriteIndented = false
        };
    }
}
