using System.Text.Json;
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
[JsonConverter(typeof(MessageCardJsonConverter))]
public record MessageCard(
    string? Summary = null,
    string? ThemeColor = null,
    string? Title = null,
    string? Text = null,
    IReadOnlyList<MessageSection>? Sections = null,
    IReadOnlyList<OpenUriAction>? PotentialActions = null
)
{
    public string Type => "MessageCard";

    public string Context => "http://schema.org/extensions";

    private class MessageCardJsonConverter : JsonConverter<MessageCard>
    {
        #region const
        private const string TypePropertyName = "@type";
        private const string TypeValue = "MessageCard";
        private const string ContextPropertyName = "@context";
        private const string ContextValue = "http://schema.org/extensions";
        private const string SummaryPropertyName = "summary";
        private const string ThemeColorPropertyName = "themeColor";
        private const string TitlePropertyName = "title";
        private const string TextPropertyName = "text";
        private const string SectionsPropertyName = "sections";
        private const string PotentialActionsPropertyName = "potentialAction";
        #endregion

        /// <inheritdoc/>
        public override MessageCard? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            bool hasTypeProperty = false;
            bool hasContextProperty = false;
            string? summary = null;
            string? themeColor = null;
            string? title = null;
            string? text = null;
            IReadOnlyList<MessageSection>? sections = null;
            IReadOnlyList<OpenUriAction>? potentialActions = null;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                string propertyName = reader.GetString()!;
                switch (propertyName)
                {
                    case TypePropertyName:
                        reader.Read();
                        string? type = reader.GetString();
                        if (type != TypeValue)
                            throw new JsonException($"expected \"{TypeValue}\", but \"{type}\".");
                        hasTypeProperty = true;
                        break;
                    case ContextPropertyName:
                        reader.Read();
                        string? context = reader.GetString();
                        if (context != ContextValue)
                            throw new JsonException($"expected \"{ContextValue}\", but \"{context}\".");
                        hasContextProperty = true;
                        break;
                    case SummaryPropertyName:
                        reader.Read();
                        summary = reader.GetString();
                        break;
                    case ThemeColorPropertyName:
                        reader.Read();
                        themeColor = reader.GetString();
                        break;
                    case TitlePropertyName:
                        reader.Read();
                        title = reader.GetString();
                        break;
                    case TextPropertyName:
                        reader.Read();
                        text = reader.GetString();
                        break;
                    case SectionsPropertyName:
                        reader.Read();
                        sections = JsonSerializer.Deserialize<IReadOnlyList<MessageSection>>(ref reader, options);
                        break;
                    case PotentialActionsPropertyName:
                        reader.Read();
                        potentialActions = JsonSerializer.Deserialize<IReadOnlyList<OpenUriAction>>(ref reader, options);
                        break;
                    default:
                        throw new JsonException($"Unknown property: {propertyName}");
                }
            }

            if (!hasTypeProperty)
                throw new JsonException($"\"{TypePropertyName}\" property is required.");
            if (!hasContextProperty)
                throw new JsonException($"\"{ContextPropertyName}\" property is required.");

            return new MessageCard(summary, themeColor, title, text, sections, potentialActions);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, MessageCard value, JsonSerializerOptions options)
        {
            bool ignoreNull = options.DefaultIgnoreCondition is JsonIgnoreCondition.WhenWritingDefault
                or JsonIgnoreCondition.WhenWritingNull;

            writer.WriteStartObject();

            writer.WriteString(TypePropertyName, TypeValue);
            writer.WriteString(ContextPropertyName, ContextValue);
            if (!ignoreNull || value.Summary is not null)
                writer.WriteString(SummaryPropertyName, value.Summary);
            if (!ignoreNull || value.ThemeColor is not null)
                writer.WriteString(ThemeColorPropertyName, value.ThemeColor);
            if (!ignoreNull || value.Title is not null)
                writer.WriteString(TitlePropertyName, value.Title);
            if (!ignoreNull || value.Text is not null)
                writer.WriteString(TextPropertyName, value.Text);
            if (!ignoreNull || value.Sections is not null)
            {
                writer.WritePropertyName(SectionsPropertyName);
                JsonSerializer.Serialize(writer, value.Sections, options);
            }
            if (!ignoreNull || value.PotentialActions is not null)
            {
                writer.WritePropertyName(PotentialActionsPropertyName);
                JsonSerializer.Serialize(writer, value.PotentialActions, options);
            }

            writer.WriteEndObject();
        }
    }
}
