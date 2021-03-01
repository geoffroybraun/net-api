using FluentAssertions;
using GB.NetApi.Application.Services.Translators;
using System;
using System.Globalization;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Translators
{
    public sealed class ResourceTranslatorTest
    {
        #region Fields

        private const string Key = "Key";
        private const string Value = "Value";
        private const string KeyWithParameters = "KeyWithParameters";
        private const string AssemblyName = "GB.NetApi.Application.Services.UnitTests";
        private static readonly string BaseName = $"{AssemblyName}.Resources.Resources";
        private static readonly ResourceTranslator Translator = new ResourceTranslator(new CultureInfo("en"), BaseName, AssemblyName);

        #endregion

        [Fact]
        public void Providing_a_null_culture_in_constructor_throws_an_exception()
        {
            static void action() => _ = new ResourceTranslator(null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Providing_an_invalid_key_when_getting_a_message_throws_an_exception(string key)
        {
            void action() => _ = Translator.GetString(key);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should().NotBeNull();
        }

        [Fact]
        public void Providing_an_inexisting_key_returns_a_default_value()
        {
            Translator.GetString("InexistingKey").Should().BeNull();
        }

        [Fact]
        public void Providing_an_existing_key_returns_the_expected_result()
        {
            Translator.GetString(Key).Should().Be(Value);
        }

        [Fact]
        public void Providing_an_inexisting_key_with_parameters_returns_a_default_value()
        {
            Translator.GetString("InexistingKey", new[] { "Test" }).Should().Be(default);
        }

        [Fact]
        public void Providing_an_existing_key_wuth_invalid_parameters_throws_an_exception()
        {
            var result = Translator.GetString(KeyWithParameters, "Test");

            result.Should().Be("Value: 'Test'");
        }
    }
}
