using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.Persons;
using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.Persons
{
    public sealed class FilterPersonControllerTest : BasePersonControllerTest
    {
        public FilterPersonControllerTest(PersonDataFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Throwing_an_exception_when_filtering_returns_an_internal_server_error_status_code()
        {
            var result = await PostAsync(BrokenClient, Endpoint, new FilterPersonQuery()).ConfigureAwait(false);

            result.StatusCode
                .Should()
                .Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Providing_an_empty_filter_query_returns_all_stored_persons()
        {
            var response = await PostAsync(Client, Endpoint, new FilterPersonQuery()).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content);

            result.Should()
                .NotBeNull()
                .And
                .NotBeEmpty();
        }

        [Fact]
        public async Task Not_filtering_persons_returns_a_not_found_status_code()
        {
            var query = new FilterPersonQuery() { BirthDay = BirthDay };
            var result = await PostAsync(NullClient, Endpoint, query).ConfigureAwait(false);

            result.StatusCode
                .Should()
                .Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Successfully_filtering_by_firstname_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { Firstname = Firstname };
            var response = await PostAsync(Client, Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Firstname != Firstname)
                .Should()
                .Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_by_lastname_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { Lastname = Lastname };
            var response = await PostAsync(Client, Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Lastname != Lastname)
                .Should()
                .Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_by_birth_year_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { BirthYear = BirthYear };
            var response = await PostAsync(Client, Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Birthdate.Year != BirthYear)
                .Should()
                .Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_by_birth_month_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { BirthMonth = BirthMonth };
            var response = await PostAsync(Client, Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Birthdate.Month != BirthMonth)
                .Should()
                .Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_by_birth_day_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { BirthDay = BirthDay };
            var response = await PostAsync(Client, Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Birthdate.Day != BirthDay)
                .Should()
                .Be(0);
        }
    }
}
