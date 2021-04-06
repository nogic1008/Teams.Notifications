using Xunit;
using Teams.Notifications.Entities;

namespace Teams.Notifications.Tests
{
    public class PotentialActionTest
    {
        [Fact]
        public void InitEmptyCtorTest()
        {
            var data = new PotentialAction();

            Assert.NotNull(data.Targets);
            Assert.Equal("OpenUri", data.Type);
            Assert.Equal("", data.Name);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("toto")]
        public void InitCtorTest(string name)
        {
            var data = new PotentialAction(name);

            Assert.NotNull(data.Targets);
            Assert.Equal("OpenUri", data.Type);
            Assert.Equal(name, data.Name);
        }
    }
}
