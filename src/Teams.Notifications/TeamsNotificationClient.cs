using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Teams.Notifications
{
    public class TeamsNotificationClient : IDisposable
    {
        private readonly Uri _uri;
        private readonly HttpClient _client = new();

        public TeamsNotificationClient(string url) => _uri = new Uri(url);

        public async ValueTask PostMessageAsync(MessageCard message)
        {
            byte[] utf8json = JsonSerializer.SerializeToUtf8Bytes(message);
            var content = new ByteArrayContent(utf8json);
            content.Headers.ContentType = new("application/json");

            var response = await _client.PostAsync(_uri, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public void Dispose() => _client.Dispose();
    }
}
