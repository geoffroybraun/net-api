using FluentAssertions;
using GB.NetApi.Domain.Services.Extensions;
using Xunit;

namespace GB.NetApi.Domain.Services.UnitTests.Extensions
{
    public sealed class IEnumerableExtensionTest
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("Value", true)]
        public void Checking_if_an_enumerable_is_not_null_nor_empty_returns_the_expected_result(string value, bool expectedResult)
        {
            var values = value.IsNotNullNorEmptyNorWhiteSpace() ? new[] { value } : default;

            values.IsNotNullNorEmpty()
                .Should()
                .Be(expectedResult);
        }
    }
}
