using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.IntegrationTests.DataFixtures;
using GB.NetApi.Application.Services.Queries.Persons;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.IntegrationTests.Handlers.Persons
{
    public sealed class GetSinglePersonHandlerTest : IClassFixture<PersonDataFixture>
    {
        #region Fields

        private const int ID = 1;
        private readonly PersonDataFixture Fixture;

        #endregion

        public GetSinglePersonHandlerTest(PersonDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public async Task Throwing_an_exception_when_getting_a_person_let_it_be_thrown()
        {
            Task<PersonDto> function()
            {
                var handler = new GetSinglePersonHandler(Fixture.Broken);

                return handler.ExecuteAsync(new GetSinglePersonQuery() { ID = ID });
            }
            var exception = await Assert.ThrowsAsync<NotImplementedException>(function).ConfigureAwait(false);

            exception.Should().NotBeNull();
        }

        [Fact]
        public async Task Not_getting_a_person_returns_a_default_value()
        {
            var handler = new GetSinglePersonHandler(Fixture.Null);
            var result = await handler.ExecuteAsync(new GetSinglePersonQuery() { ID = ID }).ConfigureAwait(false);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Successfully_getting_a_person_returns_the_expected_result()
        {
            var handler = new GetSinglePersonHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new GetSinglePersonQuery() { ID = ID }).ConfigureAwait(false);

            result.ID.Should().Be(ID);
        }
    }
}
