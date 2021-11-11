using System.Text.Json;
using System.Text.Json.Serialization;
using Nogic.Teams.Notifications.Entities;

namespace Nogic.Teams.Notifications.Tests.Entities;

/// <summary>
/// Unit test for <see cref="MessageCard"/>
/// </summary>
public class MessageCardTest
{
    /// <summary>Test data for <see cref="CanSerializeJson"/></summary>
    public static object?[][] TestDataForSerialize => new[]
    {
        new object?[]
        {
            null,
            JsonIgnoreCondition.Never,
            "null"
        },
        new object[]
        {
            new MessageCard(
                "Summary",
                "FF0000",
                "Title",
                "Text",
                Array.Empty<MessageSection>(),
                Array.Empty<OpenUriAction>()
            ),
            JsonIgnoreCondition.WhenWritingNull,
            "{"
            + "\"@type\":\"MessageCard\","
            + "\"@context\":\"http://schema.org/extensions\","
            + "\"summary\":\"Summary\","
            + "\"themeColor\":\"FF0000\","
            + "\"title\":\"Title\","
            + "\"text\":\"Text\","
            + "\"sections\":[],"
            + "\"potentialAction\":[]"
            + "}"
        },
        new object[]
        {
            new MessageCard(),
            JsonIgnoreCondition.Never,
            "{"
            + "\"@type\":\"MessageCard\","
            + "\"@context\":\"http://schema.org/extensions\","
            + "\"summary\":null,"
            + "\"themeColor\":null,"
            + "\"title\":null,"
            + "\"text\":null,"
            + "\"sections\":null,"
            + "\"potentialAction\":null"
            + "}"
        },
        new object[]
        {
            new MessageCard(),
            JsonIgnoreCondition.WhenWritingNull,
            "{"
            + "\"@type\":\"MessageCard\","
            + "\"@context\":\"http://schema.org/extensions\""
            + "}"
        },
    };
    [Theory]
    [MemberData(nameof(TestDataForSerialize))]
    public void CanSerializeJSON(MessageCard? card, JsonIgnoreCondition ignoreCondition, string expectedJson)
    {
        // Arrange - Act
        string json = JsonSerializer.Serialize(card, new JsonSerializerOptions(JsonConfig.Default)
        {
            DefaultIgnoreCondition = ignoreCondition,
        });

        // Assert
        json.Should().Be(expectedJson);
    }

    /// <summary>
    /// Test data for <see cref="CanDeserializeJson"/>
    /// </summary>
    public static object?[][] TestDataForDeserialize => new[]
    {
        new object?[]{ "null", null },
        new object[]
        {
            "{"
            + "\"@type\":\"MessageCard\","
            + "\"@context\":\"http://schema.org/extensions\","
            + "\"title\":\"Simple Message\","
            + "\"text\":\"Message Body\""
            + "}",
            new MessageCard(Title: "Simple Message", Text: "Message Body"),
        },
        new object[]
        {
            "{"
            + "\"@type\":\"MessageCard\","
            + "\"@context\":\"http://schema.org/extensions\","
            + "\"summary\":\"Array is Empty\","
            + "\"title\":\"Empty Array\","
            + "\"themeColor\":\"FF0000\","
            + "\"text\":\"Message Body\","
            + "\"sections\":[],"
            + "\"potentialAction\":[]"
            + "}",
            new MessageCard(
                "Array is Empty",
                "FF0000",
                "Empty Array",
                "Message Body",
                Array.Empty<MessageSection>(),
                Array.Empty<OpenUriAction>()
            ),
        },
    };
    [Theory]
    [MemberData(nameof(TestDataForDeserialize))]
    public void CanDeserializeJson(string json, MessageCard? expected)
    {
        // Arrange - Act
        var card = JsonSerializer.Deserialize<MessageCard>(json);

        // Assert
        if (expected is null)
        {
            card.Should().BeNull();
            return;
        }
        card!.Title.Should().Be(expected.Title);
        card.Text.Should().Be(expected.Text);
        card.ThemeColor.Should().Be(expected.ThemeColor);
        card.Sections.Should().Equal(expected.Sections);
        card.PotentialActions.Should().Equal(expected.PotentialActions);
    }

    [Theory]
    [InlineData("1", "*")]
    [InlineData("{}", "\"@type\" property is required.")]
    [InlineData("{\"@type\":\"MessageCard\"}", "\"@context\" property is required.")]
    [InlineData("{\"@context\":\"http://schema.org/extensions\"}", "\"@type\" property is required.")]
    [InlineData("{\"@type\":\"Message\",\"@context\":\"http://schema.org/extensions\"}", "expected \"MessageCard\", but \"Message\".")]
    [InlineData("{\"@type\":\"MessageCard\",\"@context\":\"foo\"}", "expected \"http://schema.org/extensions\", but \"foo\".")]
    [InlineData("{\"@type\":\"MessageCard\",\"@context\":\"http://schema.org/extensions\",\"foo\":\"bar\"}", "Unknown property: foo")]
    public void CannotDeserializeInvalidJson(string json, string errorMessage)
    {
        var deserialize = () => _ = JsonSerializer.Deserialize<MessageCard>(json);
        deserialize.Should().ThrowExactly<JsonException>().WithMessage(errorMessage);
    }
}
