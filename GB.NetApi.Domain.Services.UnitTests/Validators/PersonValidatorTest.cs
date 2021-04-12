using FluentAssertions;
using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Services.Validators;
using System;
using Xunit;

namespace GB.NetApi.Domain.Services.UnitTests.Validators
{
    public sealed class PersonValidatorTest
    {
        #region Fields

        private const int ID = 1;
        private const string Firstname = "Firstname";
        private const string Lastname = "Lastname";
        private static readonly DateTime Birthdate = DateTime.UtcNow.AddHours(-1);
        private static readonly PersonValidator Validator = new();

        #endregion

        [Fact]
        public void Providing_a_null_person_when_validating_returns_false()
        {
            Validator.IsValid(null, DateTime.Now).Should().BeFalse();
        }

        [Fact]
        public void Providing_a_null_person_when_unvalidating_returns_true()
        {
            Validator.IsNotValid(null, DateTime.Now).Should().BeTrue();
        }

        [Fact]
        public void Providing_an_empty_person_when_validating_returns_false()
        {
            Validator.IsValid(new Person(), DateTime.Now).Should().BeFalse();
        }

        [Fact]
        public void Providing_an_empty_person_when_unvalidating_returns_true()
        {
            Validator.IsNotValid(new Person(), DateTime.Now).Should().BeTrue();
        }

        [Fact]
        public void Providing_an_empty_person_sends_as_many_error_messages_using_events_as_required()
        {
            CheckIfEventIsSent(new Person(), 3);
        }

        [Fact]
        public void Providing_a_person_with_an_invalid_firstname_when_validating_returns_false()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = null,
                Lastname = Lastname
            };

            Validator.IsValid(person, DateTime.Now).Should().BeFalse();
        }

        [Fact]
        public void Providing_a_person_with_an_invalid_firstname_when_unvalidating_returns_true()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = null,
                Lastname = Lastname
            };

            Validator.IsNotValid(person, DateTime.Now).Should().BeTrue();
        }

        [Fact]
        public void Providing_a_person_with_an_invalid_firstname_sends_an_error_message_using_an_event()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = null,
                Lastname = Lastname
            };

            CheckIfEventIsSent(person, 1);
        }

        [Fact]
        public void Providing_a_person_with_an_invalid_lastname_when_validating_returns_false()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                Lastname = null
            };

            Validator.IsValid(person, DateTime.Now).Should().BeFalse();
        }

        [Fact]
        public void Providing_a_person_with_an_invalid_lastname_when_unvalidating_returns_true()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                Lastname = null
            };

            Validator.IsNotValid(person, DateTime.Now).Should().BeTrue();
        }

        [Fact]
        public void Providing_a_person_with_an_invalid_lastname_sends_an_error_message_using_an_event()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                Lastname = null
            };

            CheckIfEventIsSent(person, 1);
        }

        [Fact]
        public void Providing_a_person_with_an_invalid_birthdate_when_validating_returns_false()
        {
            var person = new Person()
            {
                Birthdate = DateTime.MaxValue,
                Firstname = Firstname,
                Lastname = Lastname
            };

            Validator.IsValid(person, DateTime.Now).Should().BeFalse();
        }

        [Fact]
        public void Providing_a_person_with_an_invalid_brithdate_when_unvalidating_returns_true()
        {
            var person = new Person()
            {
                Birthdate = DateTime.MaxValue,
                Firstname = Firstname,
                Lastname = Lastname
            };

            Validator.IsNotValid(person, DateTime.Now).Should().BeTrue();
        }

        [Fact]
        public void Providing_a_person_with_an_invalid_birthdate_sends_an_error_message_using_an_event()
        {
            var person = new Person()
            {
                Birthdate = DateTime.MaxValue,
                Firstname = Firstname,
                Lastname = Lastname
            };

            CheckIfEventIsSent(person, 1);
        }

        [Fact]
        public void Providing_a_valid_person_when_validating_returns_true()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                Lastname = Lastname
            };

            Validator.IsValid(person, DateTime.Now).Should().BeTrue();
        }

        [Fact]
        public void Providing_a_valid_person_when_unvalidating_returns_false()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                Lastname = Lastname
            };

            Validator.IsNotValid(person, DateTime.Now).Should().BeFalse();
        }

        [Fact]
        public void Providing_a_null_person_when_validating_with_its_ID_returns_false()
        {
            Validator.IsValidWithID(null, DateTime.UtcNow).Should().BeFalse();
        }

        [Fact]
        public void Providing_a_null_person_when_unvalidating_with_its_ID_returns_true()
        {
            Validator.IsNotValidWithID(null, DateTime.UtcNow).Should().BeTrue();
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public void Providing_an_invalid_ID_when_validating_with_its_ID_returns_false(int id)
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                ID = id,
                Lastname = Lastname
            };

            Validator.IsValidWithID(person, DateTime.UtcNow).Should().BeFalse();
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public void Providing_an_invalid_ID_when_unvalidating_with_its_ID_returns_true(int id)
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                ID = id,
                Lastname = Lastname
            };

            Validator.IsNotValidWithID(person, DateTime.UtcNow).Should().BeTrue();
        }

        [Fact]
        public void Providing_an_invalid_ID_when_validating_with_its_ID_sends_an_error_message_using_an_event()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                ID = int.MinValue,
                Lastname = Lastname
            };

            CheckIfEventIsSent(person, 1, true);
        }

        [Fact]
        public void Providing_only_a_valid_ID_when_validating_with_its_ID_returns_false()
        {
            Validator.IsValidWithID(new Person() { ID = ID }, DateTime.UtcNow).Should().BeFalse();
        }

        [Fact]
        public void Providing_only_a_valid_ID_when_unvalidating_with_its_ID_returns_true()
        {
            Validator.IsNotValid(new Person() { ID = ID }, DateTime.UtcNow).Should().BeTrue();
        }

        [Fact]
        public void Providing_a_valid_person_when_validating_with_its_ID_returns_true()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                ID = ID,
                Lastname = Lastname
            };

            Validator.IsValidWithID(person, DateTime.UtcNow).Should().BeTrue();
        }

        [Fact]
        public void Providing_a_valid_person_when_unvalidating_with_its_ID_returns_false()
        {
            var person = new Person()
            {
                Birthdate = Birthdate,
                Firstname = Firstname,
                ID = ID,
                Lastname = Lastname
            };

            Validator.IsNotValidWithID(person, DateTime.UtcNow).Should().BeFalse();
        }

        #region Private methods

        private static void CheckIfEventIsSent(Person person, int expectedEventCallsCount, bool includeID = false)
        {
            int eventCallsCount = 0;
            var validator = new PersonValidator();
            validator.SendErrorMessageEvent += (message, parameters) => eventCallsCount++;

            if (!includeID)
                _ = validator.IsValid(person, DateTime.UtcNow);

            if (includeID)
                _ = validator.IsValidWithID(person, DateTime.UtcNow);

            eventCallsCount.Should().Be(expectedEventCallsCount);
        }

        #endregion
    }
}
