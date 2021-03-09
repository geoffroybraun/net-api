using FluentAssertions;
using GB.NetApi.Application.Services.Collectors;
using System.Linq;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Collectors
{
    public sealed class MessagesCollectorTest
    {
        #region Fields

        private const string Message = "Message";

        #endregion

        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData(Message, 1)]
        public void Collecting_a_message_stores_it_or_not_based_on_its_value(string message, int expectedMessagesCount)
        {
            var collector = new MessagesCollector();
            collector.Collect(message);

            collector.Messages.Count().Should().Be(expectedMessagesCount);
        }

        [Fact]
        public void Collecting_a_message_retrieves_it()
        {
            var collector = new MessagesCollector();
            collector.Collect(Message);

            collector.Messages.First().Should().Be(Message);
        }
    }
}
