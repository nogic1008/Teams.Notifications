using System.Net.Http.Json;
using Moq.Contrib.HttpClient;
using Nogic.Teams.Notifications.Entities;

namespace Nogic.Teams.Notifications.Tests;

/// <summary>
/// Unit test for <see cref="TeamsPostService"/>
/// </summary>
public class TeamsPostServiceTest
{
    /// <summary>
    /// <see cref="TeamsPostService.PostMessageAsync"/> calls POST API with JSON.
    /// </summary>
    [Fact]
    public async Task PostMessageAsync_Calls_PostApi()
    {
        // Arrange
        var uri = new Uri("https://example.com/");
        var card = new MessageCard("summary", "red", "Error!", "oops");

        var handler = new Mock<HttpMessageHandler>();
        handler.SetupAnyRequest().ReturnsResponse(System.Net.HttpStatusCode.Created);

        // Act
        var service = new TeamsPostService(handler.CreateClient());
        var response = await service.PostMessageAsync(uri, card).ConfigureAwait(false);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        handler.VerifyRequest(async req =>
        {
            // URI
            req.RequestUri.Should().Be(uri);

            // Body
            var received = await req.Content!.ReadFromJsonAsync<MessageCard>().ConfigureAwait(false);
            received.Should().Be(card);

            return true;
        }, Times.Once());
    }
}
