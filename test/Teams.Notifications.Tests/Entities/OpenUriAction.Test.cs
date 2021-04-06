using System.Text.Json;
using FluentAssertions;
using Teams.Notifications.Entities;
using Xunit;

namespace Teams.Notifications.Tests.Entities
{
    /// <summary>
    /// Unit test for <see cref="OpenUriAction"/>
    /// </summary>
    public class OpenUriActionTest
    {
        [Fact]
        public void CanSerializeJSON()
        {
            // Arrange
            const string name = "Name";
            var targets = new[]
            {
                new OpenUriTarget("default", "http://example.com/"),
            };
            var sut = new OpenUriAction(name, targets);

            // Act
            string json = JsonSerializer.Serialize(sut, JsonConfig.Default);
            string targetJson = JsonSerializer.Serialize(targets[0], JsonConfig.Default);

            // Assert
            json.Should().Contain("\"@type\":\"OpenUri\"")
                .And.Contain("\"name\":\"" + name + "\"")
                .And.Contain($"\"targets\":[{targetJson}]");
        }

        [Fact]
        public void CanDeserializeJSON()
        {
            const string jsonFormat = "{{\"@type\":\"OpenApi\",\"name\":\"{0}\",\"targets\":[{1}]}}";

            // EmptyTargets
            {
                const string name = "EmptyTargets";
                string json = string.Format(jsonFormat, name, "");

                var sut = JsonSerializer.Deserialize<OpenUriAction>(json);

                sut.Should().NotBeNull();
                sut!.Name.Should().Be(name);
                sut.Targets.Should().BeEmpty();
            }

            // SingleTargets
            {
                const string name = "SingleTargets";
                string json = string.Format(jsonFormat, name, "{\"os\":\"default\",\"uri\":\"http://example.com/\"}");

                var sut = JsonSerializer.Deserialize<OpenUriAction>(json);

                sut.Should().NotBeNull();
                sut!.Name.Should().Be(name);
                sut.Targets.Should().HaveCount(1)
                    .And.Contain(new OpenUriTarget("http://example.com/", "default"));
            }

            // MultipleTargets
            {
                const string name = "MultipleTargets";
                string json = string.Format(jsonFormat, name,
                "{\"os\":\"default\",\"uri\":\"http://example.com/\"},"
                    + "{\"os\":\"iOS\",\"uri\":\"http://example.com/ios/\"},"
                    + "{\"os\":\"android\",\"uri\":\"http://example.com/android/\"}");

                var sut = JsonSerializer.Deserialize<OpenUriAction>(json);

                sut.Should().NotBeNull();
                sut!.Name.Should().Be(name);
                sut.Targets.Should().HaveCount(3)
                    .And.Contain(new OpenUriTarget("http://example.com/", "default"))
                    .And.Contain(new OpenUriTarget("http://example.com/ios/", "iOS"))
                    .And.Contain(new OpenUriTarget("http://example.com/android/", "android"));
            }
        }
    }
}
