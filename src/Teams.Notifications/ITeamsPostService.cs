using System.Threading;
using System.Threading.Tasks;
using Nogic.Teams.Notifications.Entities;

namespace Nogic.Teams.Notifications
{
    public interface ITeamsPostService
    {
        /// <summary>
        /// Post simple text to Microsoft Teams channel.
        /// </summary>
        /// <param name="text">Message text</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        ValueTask PostMessageAsync(string text, CancellationToken cancellationToken = default);

        /// <summary>
        /// Post title and text message to Microsoft Teams channel.
        /// </summary>
        /// <param name="title">Message Title</param>
        /// <param name="text">Message text</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        ValueTask PostMessageAsync(string title, string text, CancellationToken cancellationToken = default);

        /// <summary>
        /// Post a message to Microsoft Teams Channel.
        /// </summary>
        /// <param name="message">Message card object</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        ValueTask PostMessageAsync(MessageCard message, CancellationToken cancellationToken = default);
    }
}
