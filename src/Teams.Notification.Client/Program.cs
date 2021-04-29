using ConsoleAppFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nogic.Teams.Notifications;
using Nogic.Teams.Notifications.Client;

await Host.CreateDefaultBuilder()
    .ConfigureLogging(logging => logging.ReplaceToSimpleConsole())
    .ConfigureServices((_, services) =>
        // Dependency Injection
        services.AddHttpClient<ITeamsPostService, TeamsPostService>())
    .RunConsoleAppFrameworkAsync<AppBase>(args)
    .ConfigureAwait(false);
