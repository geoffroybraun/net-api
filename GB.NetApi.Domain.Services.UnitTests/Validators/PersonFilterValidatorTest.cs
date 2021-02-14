using FluentAssertions;
using GB.NetApi.Domain.Models.Entities.Filters;
using GB.NetApi.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Domain.Services.UnitTests.Validators
{
    public sealed class PersonFilterValidatorTest
    {
        [Fact]
        public void Providing_a_null_filter_when_validating_returns_false()
        {
            PersonFilterValidator.IsValid(null)
                .Should()
                .BeFalse();
        }
        [Fact]
        public void Providing_a_null_filter_when_unvalidating_returns_true()
        {
            PersonFilterValidator.IsNotValid(null)
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Providing_at_least_a_firstname_when_validating_returns_true()
        {
            PersonFilterValidator.IsValid(new PersonFilter() { Firstname = "Firstname" })
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Providing_at_least_a_firstname_when_unvalidating_returns_false()
        {
            PersonFilterValidator.IsNotValid(new PersonFilter() { Firstname = "Firstname" })
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Providing_at_least_a_lastname_when_validating_returns_true()
        {
            PersonFilterValidator.IsValid(new PersonFilter() { Lastname = "Lastname" })
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Providing_at_least_a_lastname_when_unvalidating_returns_false()
        {
            PersonFilterValidator.IsNotValid(new PersonFilter() { Lastname = "Lastname" })
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Providing_at_least_a_birth_year_when_validating_returns_true()
        {
            PersonFilterValidator.IsValid(new PersonFilter() { BirthYear = int.MaxValue })
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Providing_at_least_a_birth_year_when_unvalidating_returns_false()
        {
            PersonFilterValidator.IsNotValid(new PersonFilter() { BirthYear = int.MaxValue })
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Providing_at_least_a_birth_month_when_validating_returns_true()
        {
            PersonFilterValidator.IsValid(new PersonFilter() { BirthMonth = 1 })
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Providing_at_least_a_birth_month_when_unvalidating_returns_false()
        {
            PersonFilterValidator.IsNotValid(new PersonFilter() { BirthMonth = 1 })
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Providing_an_invalid_birth_month_when_validating_returns_false()
        {
            PersonFilterValidator.IsValid(new PersonFilter() { BirthMonth = int.MaxValue })
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Providing_an_invalid_birth_month_when_unvalidating_returns_true()
        {
            PersonFilterValidator.IsNotValid(new PersonFilter() { BirthMonth = int.MaxValue })
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Providing_at_least_a_birth_day_when_validating_returns_true()
        {
            PersonFilterValidator.IsValid(new PersonFilter() { BirthDay = 1 })
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Providing_at_least_a_birth_day_when_unvalidating_returns_false()
        {
            PersonFilterValidator.IsNotValid(new PersonFilter() { BirthDay = 1 })
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Providing_an_invalid_birth_day_when_validating_returns_false()
        {
            PersonFilterValidator.IsValid(new PersonFilter() { BirthDay = int.MaxValue })
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Providing_an_invalid_birth_day_when_unvalidating_returns_true()
        {
            PersonFilterValidator.IsNotValid(new PersonFilter() { BirthDay = int.MaxValue })
                .Should()
                .BeTrue();
        }
    }
}
