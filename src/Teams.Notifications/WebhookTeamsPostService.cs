using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Nogic.Teams.Notifications.Entities;

namespace Nogic.Teams.Notifications
{
    /// <summary>
    /// Implementation of <see cref="ITeamsPostService"/> with Incoming Webhook
    /// </summary>
    public class WebhookTeamsPostService : ITeamsPostService
    {
        private readonly Uri _webhookUri;
        private readonly HttpClient _client;

        public WebhookTeamsPostService(string webhookUri, HttpClient client)
            => (_webhookUri, _client) = (new(webhookUri), client);
        public WebhookTeamsPostService(Uri webhookUri, HttpClient client)
            => (_webhookUri, _client) = (webhookUri, client);

        /// <inheritdoc/>
        public ValueTask PostMessageAsync(string text, CancellationToken cancellationToken = default)
            => PostMessageAsync(new MessageCard(Text: text), cancellationToken);

        /// <inheritdoc/>
        public ValueTask PostMessageAsync(string title, string text, CancellationToken cancellationToken = default)
            => PostMessageAsync(new MessageCard(Title: title, Text: text), cancellationToken);

        /// <inheritdoc/>
        public async ValueTask PostMessageAsync(MessageCard message, CancellationToken cancellationToken = default)
        {
            var response = await _client.PostAsJsonAsync(_webhookUri, message, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
