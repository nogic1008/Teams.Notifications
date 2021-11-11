using System.Text.Json.Serialization;

namespace Nogic.Teams.Notifications.Entities;

/// <summary>
/// Entity class for Microsoft Teams message.
/// <see href="https://docs.microsoft.com/outlook/actionable-messages/message-card-reference#card-fields"/>
/// </summary>
/// <param name="Summary">
/// Required if <paramref name="Text"/> is <see langword="null"/>, otherwise optional.
/// This is typically displayed in the list view in Outlook, as a way to quickly determine what the card is all about.
/// <para>
/// <list type="bullet">
///   <item>DO always include a summary.</item>
///   <item>
///   DO NOT include details in the summary.
///   For example, for a Twitter post, a summary might simply read "New tweet from @someuser" without mentioning the content of the tweet itself.
///   </item>
/// </list>
/// </para>
/// </param>
/// <param name="ThemeColor">
/// Specifies a custom brand color for the card.
/// The color will be displayed in a non-obtrusive manner.
/// <para>
/// <list type="bullet">
///   <item>DO use this to brand cards to your color.</item>
///   <item>DO NOT use this to indicate status.</item>
/// </list>
/// </para>
/// </param>
/// <param name="Title">
/// This is meant to be rendered in a prominent way, at the very top of the card.
/// Use it to introduce the content of the card in such a way users will immediately know what to expect.
/// <para>
/// <list type="bullet">
///   <item>DO keep title short, don't make it a long sentence.</item>
///   <item>DO mention the name of the entity being referenced in the title.</item>
///   <item>DO NOT use hyperlinks (via Markdown) in the title.</item>
/// </list>
/// </para>
/// <para>
/// Examples:
/// <example>
/// <list type="bullet">
///   <item>Daily news</item>
///   <item>New bug opened</item>
///   <item>Task [name of task] assigned</item>
/// </list>
/// </example>
/// </para>
/// </param>
/// <param name="Text">
/// Required if <paramref name="Summary"/> is <see langword="null"/>, otherwise optional.
/// This is meant to be displayed in a normal font below the card's title.
/// Use it to display content, such as the description of the entity being referenced, or an abstract of a news article.
/// <para>
/// <list type="bullet">
///   <item>DO use simple Markdown, such as bold or italics to emphasize words, and links to external resources.</item>
///   <item>
///     DO NOT include any call to action in the text property.
///     Users should be able to not read it and still understand what the card is all about.
///   </item>
/// </list>
/// </para>
/// </param>
/// <param name="Sections">A collection of sections to include in the card.</param>
/// <param name="PotentialActions">A collection of actions that can be invoked on this card.</param>
public record MessageCard(
    [property: JsonPropertyName("summary")] string? Summary = null,
    [property: JsonPropertyName("themeColor")] string? ThemeColor = null,
    [property: JsonPropertyName("title")] string? Title = null,
    [property: JsonPropertyName("text")] string? Text = null,
    [property: JsonPropertyName("sections")] IList<MessageSection>? Sections = null,
    [property: JsonPropertyName("potentialAction")] IList<OpenUriAction>? PotentialActions = null
)
{
    [JsonPropertyName("@type")]
    public string Type => "MessageCard";

    [JsonPropertyName("@context")]
    public string Context => "http://schema.org/extensions";
}
