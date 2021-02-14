using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.IntegrationTests.DataFixtures;
using GB.NetApi.Application.Services.Queries.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.Persons
{
    public sealed class FilterPersonHandlerTest : IClassFixture<PersonDataFixture>
    {
        #region Fields

        private const string Firstname = "Firstname";
        private const string Lastname = "Lastname";
        private const int BirthYear = 1990;
        private const int BirthMonth = 1;
        private const int BirthDay = 1;
        private readonly PersonDataFixture Fixture;

        #endregion

        public FilterPersonHandlerTest(PersonDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public async Task Throwing_an_exception_let_it_be_thrown()
        {
            Task<IEnumerable<PersonDto>> function()
            {
                var handler = new FilterPersonHandler(Fixture.Broken);

                return handler.ExecuteAsync(new FilterPersonQuery());
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_filtering_persons_returns_a_default_value()
        {
            var handler = new FilterPersonHandler(Fixture.Null);
            var result = await handler.ExecuteAsync(new FilterPersonQuery()).ConfigureAwait(false);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Successfully_filtering_persons_without_any_value_returns_them_all()
        {
            var handler = new FilterPersonHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new FilterPersonQuery()).ConfigureAwait(false);

            result.Should().NotBeNull().And.NotBeEmpty();
        }

        [Fact]
        public async Task Successfully_filtering_persons_by_firstname_returns_the_expected_result()
        {
            var handler = new FilterPersonHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new FilterPersonQuery() { Firstname = Firstname }).ConfigureAwait(false);

            result.Count(r => r.Firstname != Firstname).Should().Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_persons_by_lastname_returns_the_expected_result()
        {
            var handler = new FilterPersonHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new FilterPersonQuery() { Lastname = Lastname }).ConfigureAwait(false);

            result.Count(r => r.Lastname != Lastname).Should().Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_persons_by_birth_year_returns_the_expected_result()
        {
            var handler = new FilterPersonHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new FilterPersonQuery() { BirthYear = BirthYear }).ConfigureAwait(false);

            result.Count(r => r.Birthdate.Year != BirthYear).Should().Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_persons_by_birth_month_returns_the_expected_result()
        {
            var handler = new FilterPersonHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new FilterPersonQuery() { BirthMonth = BirthMonth }).ConfigureAwait(false);

            result.Count(r => r.Birthdate.Month != BirthMonth).Should().Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_persons_by_birth_day_returns_the_expected_result()
        {
            var handler = new FilterPersonHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new FilterPersonQuery() { BirthDay = BirthDay }).ConfigureAwait(false);

            result.Count(r => r.Birthdate.Day != BirthDay).Should().Be(0);
        }
    }
}
