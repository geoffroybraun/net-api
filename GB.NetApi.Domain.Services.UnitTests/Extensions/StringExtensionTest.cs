using FluentAssertions;
using GB.NetApi.Domain.Services.Extensions;
using Xunit;

namespace GB.NetApi.Domain.Services.UnitTests.Extensions
{
    public sealed class StringExtensionTest
    {
        [Theory]
        [InlineData(null, "Value", true)]
        [InlineData("", "Value", true)]
        [InlineData(" ", "Value", true)]
        [InlineData("Value", null, true)]
        [InlineData("Value", "", true)]
        [InlineData("Value", " ", true)]
        [InlineData("Value", "value", true)]
        [InlineData("Value", "VALUE", true)]
        [InlineData("Value", "Value", false)]
        public void Checking_if_a_string_value_is_not_equal_to_another_one_returns_the_expected_result(string value, string other, bool expectedResult)
        {
            value.IsNotEqualTo(other).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData("Value", false)]
        public void Checking_if_a_string_value_is_null_or_empty_or_white_space_returns_the_expected_result(string value, bool expectedResult)
        {
            value.IsNullOrEmptyOrWhiteSpace().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("Value", true)]
        public void Checking_if_a_string_value_is_not_null_nor_empty_nor_white_space_returns_the_expected_result(string value, bool expectedResult)
        {
            value.IsNotNullNorEmptyNorWhiteSpace().Should().Be(expectedResult);
        }
    }
}
