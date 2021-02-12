using FluentAssertions;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.Queries.Persons;
using GB.NetApi.Application.Services.UnitTests.DataFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.Persons
{
    public sealed class GetSinglePersonHandlerTest : IClassFixture<PersonDataFixture>
    {
        #region Fields

        private readonly PersonDataFixture Fixture;

        #endregion

        public GetSinglePersonHandlerTest(PersonDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public void Providing_a_null_repository_in_constructor_throws_an_exception()
        {
            static void action() => _ = new GetSinglePersonHandler(null);
            var exception = Assert.Throws<ArgumentNullException>(action);

            exception.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_query_to_execute_throws_an_exception()
        {
            Task<PersonDto> function()
            {
                var handler = new GetSinglePersonHandler(Fixture.Dummy);

                return handler.ExecuteAsync(null);
            }
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(function).ConfigureAwait(false);

            exception.Should()
                .NotBeNull();
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Providing_an_invalid_query_to_execute_throws_an_exception(int id)
        {
            Task<PersonDto> function()
            {
                var handler = new GetSinglePersonHandler(Fixture.Dummy);

                return handler.ExecuteAsync(new GetSinglePersonQuery() { ID = id });
            }
            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(function).ConfigureAwait(false);

            exception.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task Providing_a_valid_query_returns_the_expected_result()
        {
            var handler = new GetSinglePersonHandler(Fixture.Dummy);
            var result = await handler.ExecuteAsync(new GetSinglePersonQuery() { ID = 1 }).ConfigureAwait(false);

            result.Should()
                .NotBeNull();
        }
    }
}
