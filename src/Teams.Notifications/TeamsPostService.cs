using System.Net.Http.Json;
using Nogic.Teams.Notifications.Entities;

namespace Nogic.Teams.Notifications;

/// <summary>
/// Implementation of <see cref="ITeamsPostService"/>
/// </summary>
public class TeamsPostService : ITeamsPostService
{
    private readonly HttpClient _client;

    public TeamsPostService(HttpClient client)
        => _client = client;

    /// <inheritdoc/>
    public async ValueTask<HttpResponseMessage> PostMessageAsync(Uri webhookUri, MessageCard message, CancellationToken cancellationToken = default)
        => await _client.PostAsJsonAsync(webhookUri, message, cancellationToken).ConfigureAwait(false);
}
