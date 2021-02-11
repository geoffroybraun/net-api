using FluentAssertions;
using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers
{
    public sealed class PersonControllerTest : BaseControllerTest
    {
        #region Fields

        private const string Endpoint = "persons";
        private const string Firstname = "Firstname";
        private const string Lastname = "Lastname";
        private const int BirthYear = 1990;
        private const int BirthMonth = 1;
        private const int BirthDay = 1;

        #endregion

        [Fact]
        public async Task Providing_an_invalid_person_to_create_returns_a_bad_request_status_code()
        {
            var result = await PutAsync(Endpoint, new CreatePersonCommand())
                .ConfigureAwait(false);

            result.StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Successfully_creating_a_person_returns_true()
        {
            var command = new CreatePersonCommand() { Birthdate = DateTime.Now, Firstname = "New firstname", Lastname = "New lastname" };
            var result = await PutAsync(Endpoint, command)
                .ConfigureAwait(false);

            result.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Providing_an_empty_filter_query_returns_all_stored_persons()
        {
            var response = await PostAsync(Endpoint, new FilterPersonQuery()).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content);

            result.Should()
                .NotBeNull()
                .And
                .NotBeEmpty();
        }

        [Fact]
        public async Task Successfully_filtering_by_firstname_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { Firstname = Firstname };
            var response = await PostAsync(Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Firstname != Firstname)
                .Should()
                .Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_by_lastname_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { Lastname = Lastname };
            var response = await PostAsync(Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Lastname != Lastname)
                .Should()
                .Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_by_birth_year_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { BirthYear = BirthYear };
            var response = await PostAsync(Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Birthdate.Year != BirthYear)
                .Should()
                .Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_by_birth_month_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { BirthMonth = BirthMonth };
            var response = await PostAsync(Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Birthdate.Month != BirthMonth)
                .Should()
                .Be(0);
        }

        [Fact]
        public async Task Successfully_filtering_by_birth_day_returns_the_expected_result()
        {
            var query = new FilterPersonQuery() { BirthDay = BirthDay };
            var response = await PostAsync(Endpoint, query).ConfigureAwait(false);
            var result = await DeserializeContentAsync<IEnumerable<PersonDto>>(response.Content).ConfigureAwait(false);

            result.Count(r => r.Birthdate.Day != BirthDay)
                .Should()
                .Be(0);
        }
    }
}
