using FluentAssertions;
using GB.NetApi.Domain.Services.Extensions;
using Xunit;

namespace GB.NetApi.Domain.Services.UnitTests.Extensions
{
    public sealed class IntegerExtensionTest
    {
        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(0, 1, true)]
        [InlineData(1, 0, false)]
        [InlineData(1, 1, true)]
        public void Checking_if_the_extended_int_value_is_inferior_or_equal_to_the_other_one_returns_the_expected_result(int value, int other, bool expectedResult)
        {
            value.IsInferiorOrEqualTo(other)
                .Should()
                .Be(expectedResult);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0, 1, false)]
        [InlineData(1, 0, true)]
        [InlineData(1, 1, false)]
        public void Checking_if_the_extended_int_value_is_superior_to_the_other_one_returns_the_expected_result(int value, int other, bool expectedResult)
        {
            value.IsSuperiorTo(other)
                .Should()
                .Be(expectedResult);
        }
    }
}
