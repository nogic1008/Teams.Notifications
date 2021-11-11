using Nogic.Teams.Notifications.Entities;

namespace Nogic.Teams.Notifications;

public interface ITeamsPostService
{
    /// <summary>
    /// Post a message to Microsoft Teams Channel.
    /// </summary>
    /// <param name="webhookUri">Incoming Webhook URI</param>
    /// <param name="message">Message card object</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    ValueTask<HttpResponseMessage> PostMessageAsync(Uri webhookUri, MessageCard message, CancellationToken cancellationToken = default);
}
