using ConsoleAppFramework;
using Nogic.Teams.Notifications.Entities;
using Microsoft.Extensions.Logging;
using Nogic.Teams.Notifications;

namespace ConsoleAppSample;

public class AppBase : ConsoleAppBase
{
    private readonly ITeamsPostService _postService;
    public AppBase(ITeamsPostService postService)
        => _postService = postService;

    public async ValueTask PostAsync(string uri, string title, string description)
    {
        var webhookUri = new Uri(uri);
        var message = new MessageCard(Title: title, Text: description);

        var res = await _postService.PostMessageAsync(webhookUri, message, Context.CancellationToken).ConfigureAwait(false);
        res.EnsureSuccessStatusCode();

        Context.Logger.LogInformation("Posted successfully to Teams channel.");
    }
}
