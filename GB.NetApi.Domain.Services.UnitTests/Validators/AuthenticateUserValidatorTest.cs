using FluentAssertions;
using GB.NetApi.Domain.Services.Validators;
using Xunit;

namespace GB.NetApi.Domain.Services.UnitTests.Validators
{
    public sealed class AuthenticateUserValidatorTest
    {
        #region Fields

        private const string UserName = "Name";
        private const string UserEmail = "Email";
        private static readonly AuthenticateUserValidator Validator = new AuthenticateUserValidator();

        #endregion

        [Theory]
        [InlineData(null, null, false)]
        [InlineData(null, UserEmail, false)]
        [InlineData("", UserEmail, false)]
        [InlineData(" ", UserEmail, false)]
        [InlineData(UserName, null, false)]
        [InlineData(UserName, "", false)]
        [InlineData(UserName, " ", false)]
        [InlineData(UserName, UserEmail, true)]
        public void Providing_user_name_or_address_when_validating_returns_the_expected_result(string userName, string userEmail, bool expectedResult)
        {
            Validator.IsValid(userName, userEmail).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null, null, true)]
        [InlineData(null, UserEmail, true)]
        [InlineData("", UserEmail, true)]
        [InlineData(" ", UserEmail, true)]
        [InlineData(UserName, null, true)]
        [InlineData(UserName, "", true)]
        [InlineData(UserName, " ", true)]
        [InlineData(UserName, UserEmail, false)]
        public void Providing_user_name_or_address_when_unvalidating_returns_the_expected_result(string userName, string userEmail, bool expectedResult)
        {
            Validator.IsNotValid(userName, userEmail).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null, null, 2)]
        [InlineData(null, UserEmail, 1)]
        [InlineData("", UserEmail, 1)]
        [InlineData(" ", UserEmail, 1)]
        [InlineData(UserName, null, 1)]
        [InlineData(UserName, "", 1)]
        [InlineData(UserName, " ", 1)]
        [InlineData(UserName, UserEmail, 0)]
        public void Providing_user_name_and_email_address_sends_an_error_message_using_an_event_in_case_of_failure(string userName, string userEmail, int expectedEventCallsCount)
        {
            CheckIfEventIsSent(userName, userEmail, expectedEventCallsCount);
        }

        #region Private methods

        private static void CheckIfEventIsSent(string userName, string userEmail, int expectedEventCallsCount)
        {
            int eventCallsCount = 0;
            var validator = new AuthenticateUserValidator();
            validator.SendErrorMessageEvent += (message, parameters) => eventCallsCount++;
            _ = validator.IsValid(userName, userEmail);

            eventCallsCount.Should().Be(expectedEventCallsCount);
        }

        #endregion
    }
}
