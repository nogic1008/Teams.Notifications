using System.Text.Json.Serialization;

namespace Nogic.Teams.Notifications.Entities;

/// <summary>
/// Entity class for <see cref="MessageCard.Sections"/>
/// <see href="https://docs.microsoft.com/outlook/actionable-messages/message-card-reference#section-fields"/>
/// </summary>
/// <param name="Title">
/// This is displayed in a font that stands out while not as prominent as <see cref="MessageCard.Title"/>.
/// It is meant to introduce the section and summarize its content, similarly to how <see cref="MessageCard.Title"/> is meant to summarize the whole card.
/// <para>
/// <list type="bullet">
///   <item>DO keep title short, don't make it a long sentence.</item>
///   <item>DO mention the name of the entity being referenced in the title.</item>
///   <item>DO NOT use hyperlinks (via Markdown) in the title.</item>
/// </list>
/// </para>
/// </param>
/// <param name="StartGroup">
/// When set to <see langword="true"/>, this marks the start of a logical group of information.
/// Typically, <paramref name="StartGroup"/> set to <see langword="true"/> will be visually separated from previous card elements.
/// For example, Outlook uses a subtle horizontal separation line.
/// </param>
/// <param name="ActivityImage">
/// Forms a logical group.
/// <paramref name="ActivityTitle"/>, <paramref name="ActivitySubtitle"/> and <paramref name="ActivityText"/> will be displayed alongside <paramref name="ActivityImage"/>,
/// using a layout appropriate for the form factor of the device the card is being viewed on.
/// For instance, in Outlook on the Web, <paramref name="ActivityTitle"/>, <paramref name="ActivitySubtitle"/> and <paramref name="ActivityText"/> are displayed on the right of <paramref name="ActivityImage"/>, using a two-column layout.
/// <para>
/// Use the activity fields for scenarios such as:
/// <list type="table">
///   <item>
///     <term>Someone did something</term>
///     <description>
///       Use <paramref name="ActivityImage"/> to display the picture of that person.
///     </description>
///   </item>
///   <item>
///     <term>A news article abstract</term>
///     <description>
///       Use <paramref name="ActivityImage"/> to display the picture associated with the article
///     </description>
///   </item>
/// </list>
/// </para>
/// </param>
/// <param name="ActivityTitle">
/// Forms a logical group.
/// <paramref name="ActivityTitle"/>, <paramref name="ActivitySubtitle"/> and <paramref name="ActivityText"/> will be displayed alongside <paramref name="ActivityImage"/>,
/// using a layout appropriate for the form factor of the device the card is being viewed on.
/// For instance, in Outlook on the Web, <paramref name="ActivityTitle"/>, <paramref name="ActivitySubtitle"/> and <paramref name="ActivityText"/> are displayed on the right of <paramref name="ActivityImage"/>, using a two-column layout.
/// <para>
/// Use the activity fields for scenarios such as:
/// <list type="table">
///   <item>
///     <term>Someone did something</term>
///     <description>
///       Use <paramref name="ActivityTitle"/> to summarize what they did. Make it short and to the point.
///     </description>
///   </item>
/// </list>
/// </para>
/// </param>
/// <param name="ActivitySubtitle">
/// Forms a logical group.
/// <paramref name="ActivityTitle"/>, <paramref name="ActivitySubtitle"/> and <paramref name="ActivityText"/> will be displayed alongside <paramref name="ActivityImage"/>,
/// using a layout appropriate for the form factor of the device the card is being viewed on.
/// For instance, in Outlook on the Web, <paramref name="ActivityTitle"/>, <paramref name="ActivitySubtitle"/> and <paramref name="ActivityText"/> are displayed on the right of <paramref name="ActivityImage"/>, using a two-column layout.
/// <para>
/// Use the activity fields for scenarios such as:
/// <list type="table">
///   <item>
///     <term>Someone did something</term>
///     <description>
///       Use <paramref name="ActivitySubtitle"/> to show, for instance, the date and time the action was taken, or the person's handle.
///       <list type="bullet">
///         <item><paramref name="ActivitySubtitle"/> will be rendered in a more subdued font</item>
///         <item>DO NOT include essential information</item>
///         <item>DO NOT include calls to action</item>
///         <item>AVOID Markdown formatting</item>
///       </list>
///     </description>
///   </item>
///   <item>
///     <term>A news article abstract</term>
///     <description>
///       Use <paramref name="ActivitySubtitle"/> to display the date and time the article was originally posted
///     </description>
///   </item>
/// </list>
/// </para>
/// </param>
/// <param name="ActivityText">
/// Forms a logical group.
/// <paramref name="ActivityTitle"/>, <paramref name="ActivitySubtitle"/> and <paramref name="ActivityText"/> will be displayed alongside <paramref name="ActivityImage"/>,
/// using a layout appropriate for the form factor of the device the card is being viewed on.
/// For instance, in Outlook on the Web, <paramref name="ActivityTitle"/>, <paramref name="ActivitySubtitle"/> and <paramref name="ActivityText"/> are displayed on the right of <paramref name="ActivityImage"/>, using a two-column layout.
/// <para>
/// Use the activity fields for scenarios such as:
/// <list type="table">
///   <item>
///     <term>Someone did something</term>
///     <description>
///       Use <paramref name="ActivityText"/> to provide details about the activity.
///       <list type="bullet">
///         <item>DO use simple Markdown to emphasize words or link to external sources</item>
///         <item>DO NOT include calls to action</item>
///       </list>
///     </description>
///   </item>
///   <item>
///     <term>A news article abstract</term>
///     <description>Use <paramref name="ActivityText"/> to display the actual abstract</description>
///   </item>
/// </list>
/// </para>
/// </param>
/// <param name="HeroImage">Use this to make an image the centerpiece of your card.</param>
/// <param name="Text">This is very similar to <see cref="MessageCard.Text"/>. It can be used for the same purpose.</param>
/// <param name="Facts">
/// They are a very important component of a section.
/// They often contain the information that really matters to the user.
/// <para>
/// Facts are displayed in such a way that they can be read quickly and efficiently.
/// For example, in Outlook on the Web, facts are presented in a two-column layout, with <see cref="MessageFact.Name"/> rendered in a slightly more prominent font:
/// </para>
/// <para>
/// <list type="bullet">
///   <item>DO use them instead of embedding important information inside <paramref name="Text"/> or <see cref="MessageCard.Text"/>.</item>
///   <item>DO keep <see cref="MessageFact.Name"/> short.</item>
///   <item>AVOID making <see cref="MessageFact.Value"/> too long.</item>
///   <item>
///     AVOID using Markdown formatting for both <see cref="MessageFact.Name"/> and <see cref="MessageFact.Value"/>.
///     Let facts be rendered as intended as that is how they will have the most impact.
///   </item>
///   <item>
///     DO however use Markdown for links in <see cref="MessageFact.Value"/> only.
///     For instance, if a fact references an external document, make the value of that fact a link to the document.
///   </item>
///   <item>
///     DO NOT add a fact without a real purpose.
///     For instance, a fact that would always have the same value across all cards is not interesting and a waste of space.
///   </item>
/// </list>
/// </para>
/// </param>
/// <param name="Images">
/// This allows for the inclusion of a photo gallery inside a section.
/// That photo gallery will always be displayed in a way that is easy to consume regardless of the form factor of the device it is being viewed on.
/// </param>
/// <param name="PotentialActions">A collection of actions that can be invoked on this section.</param>
public record MessageSection(
    [property: JsonPropertyName("title")] string? Title = null,
    [property: JsonPropertyName("startGroup")] bool? StartGroup = null,
    [property: JsonPropertyName("activityImage")] string? ActivityImage = null,
    [property: JsonPropertyName("activityTitle")] string? ActivityTitle = null,
    [property: JsonPropertyName("activitySubtitle")] string? ActivitySubtitle = null,
    [property: JsonPropertyName("activityText")] string? ActivityText = null,
    [property: JsonPropertyName("heroImage")] SectionImage? HeroImage = null,
    [property: JsonPropertyName("text")] string? Text = null,
    [property: JsonPropertyName("facts")] IList<MessageFact>? Facts = null,
    [property: JsonPropertyName("images")] IList<SectionImage>? Images = null,
    [property: JsonPropertyName("potentialAction")] IList<OpenUriAction>? PotentialActions = null
);

/// <summary>
/// Defines an image as used by <see cref="MessageSection.HeroImage"/> and <see cref="MessageSection.Images"/>.
/// <see href="https://docs.microsoft.com/outlook/actionable-messages/message-card-reference#image-object"/>
/// </summary>
/// <param name="Image">The URL to the image.</param>
/// <param name="Title">
/// A short description of the image.
/// Typically, it is displayed in a tooltip as the user hovers their mouse over the image.
/// </param>
public record SectionImage(
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("title")] string Title
);

/// <summary>
/// Contains the information that really matters to the user.
/// </summary>
public record MessageFact(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("value")] string Value
);
