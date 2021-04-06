using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Notifications.Entities;

namespace Teams.Notifications.Client
{
    internal static class Program
    {
        private async static Task Main()
        {
            const string url = "";
            var client = new TeamsNotificationClient(url);

            var message = new MessageCard(
                Title: "title",
                Text: "text",
                ThemeColor: "f0ad4e",
                Sections: new List<MessageSection>
                {
                    new (
                        Title: "Context",
                        Facts: new List<MessageFact>
                        {
                            new ("Key", "Value")
                        }
                    )
                },
                PotentialActions: new List<OpenApiAction>
                {
                    new (
                        Name: "Open",
                        Targets: new List<OpenApiTarget>
                        {
                            new ("http://google.com")
                        }
                    )
                }
            );
            await client.PostMessageAsync(message).ConfigureAwait(false);
        }
    }
}
