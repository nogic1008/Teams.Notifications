using System.Text.Json.Serialization;

namespace Nogic.Teams.Notifications.Entities;

/// <summary>
/// Opens a URI in a separate browser or app.
/// https://docs.microsoft.com/outlook/actionable-messages/message-card-reference#openuri-action
/// <para>
/// Although links can be achieved through Markdown, <see cref="OpenUriAction"/> has the advantage of allowing you to specify different URIs for different operating systems, which makes it possible to open the link in an app on mobile devices.
/// Consider using <see cref="OpenUriAction"/> rather than a link in Markdown if there is a clear advantage for your users in their ability to open the link in an app on their mobile device.
/// </para>
/// </summary>
/// <remarks>
/// **Do** include at least <see cref="OpenUriAction"/> to view the entity in the external app it comes from.
/// **Do** make <see cref="OpenUriAction"/> the last one in the potentialAction collection.
/// <para>
/// Note: Microsoft Teams only support HTTP/HTTPS URLs in <see cref="OpenUriTarget.Uri"/>
/// </para>
/// </remarks>
/// <param name="Name">
/// Defines the text that will be displayed on screen for the action.
/// <para>
/// <list type="bullet">
///   <item>
///     DO use verbs. For instance, use "Set due date" instead of "Due date" or "Add note" instead of "Note."
///     In some cases, the noun itself just works because it is also a verb: "Comment"
///   </item>
///   <item>
///     DO NOT name an OpenUri action in such a way that it suggests the action can be taken right from the client.
///     Instead, name the action "View in [name of site/app]" or "Open in [name of site/app]"
///   </item>
/// </list>
/// </para>
/// </param>
/// <param name="Targets">Name/value pairs that defines one URI per target operating system.</param>
public record OpenUriAction(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("targets")] IList<OpenUriTarget> Targets
)
{
    [JsonPropertyName("@type")]
    public string Type => "OpenUri";
}

/// <summary><inheritdoc cref="OpenUriAction" path="/param[@name='Targets']"/></summary>
/// <param name="OS">
/// Target operating system.
/// Supported values are <c>"default"</c>, <c>"windows"</c>, <c>"iOS"</c> and <c>"android"</c>.
/// The <c>"default"</c> operating system will in most cases simply open the URI in a web browser, regardless of the actual operating system.
/// </param>
public record OpenUriTarget(
    [property: JsonPropertyName("uri")] string Uri,
    [property: JsonPropertyName("os")] string OS = "default"
);
