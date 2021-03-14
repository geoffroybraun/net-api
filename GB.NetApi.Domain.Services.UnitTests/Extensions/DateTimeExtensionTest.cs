using FluentAssertions;
using GB.NetApi.Domain.Services.Extensions;
using System;
using Xunit;

namespace GB.NetApi.Domain.Services.UnitTests.Extensions
{
    public sealed class DateTimeExtensionTest
    {
        [Fact]
        public void Checking_if_an_extended_datetime_value_is_not_equal_to_another_one_returns_the_expected_result()
        {
            var values = new Tuple<DateTime, DateTime, bool>[]
            {
                new Tuple<DateTime, DateTime, bool>(DateTime.MinValue, DateTime.MinValue, false),
                new Tuple<DateTime, DateTime, bool>(DateTime.MinValue, DateTime.MaxValue, true),
                new Tuple<DateTime, DateTime, bool>(DateTime.MaxValue, DateTime.MinValue, true),
                new Tuple<DateTime, DateTime, bool>(DateTime.MaxValue, DateTime.MaxValue, false),
            };

            var result = true;

            foreach (var value in values)
                result &= value.Item1.IsNotEqualTo(value.Item2) == value.Item3;

            result.Should().BeTrue();
        }

        [Fact]
        public void Checking_if_an_extended_datetime_value_is_superior_to_another_one_returns_the_expected_result()
        {
            var values = new Tuple<DateTime, DateTime, bool>[]
            {
                new Tuple<DateTime, DateTime, bool>(DateTime.MinValue, DateTime.MinValue, false),
                new Tuple<DateTime, DateTime, bool>(DateTime.MinValue, DateTime.MaxValue, false),
                new Tuple<DateTime, DateTime, bool>(DateTime.MaxValue, DateTime.MinValue, true),
                new Tuple<DateTime, DateTime, bool>(DateTime.MaxValue, DateTime.MaxValue, false),
            };

            var result = true;

            foreach (var value in values)
                result &= value.Item1.IsSuperiorTo(value.Item2) == value.Item3;

            result.Should().BeTrue();
        }
    }
}
