using FluentAssertions;
using GB.NetApi.Application.Services.Handlers.Persons;
using GB.NetApi.Application.Services.Queries.Persons;
using GB.NetApi.Application.Services.UnitTests.DataFixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GB.NetApi.Application.Services.UnitTests.Handlers.Persons
{
    public sealed class FilterPersonHandlerTest : IClassFixture<PersonDataFixture>
    {
        #region Fields

        private readonly PersonDataFixture Fixture;

        #endregion

        public FilterPersonHandlerTest(PersonDataFixture fixture) => Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));

        [Fact]
        public void Providing_a_null_repository_in_constructor_throws_an_exception()
        {
            static void action() => _ = new FilterPersonHandler(null);

            Assert.Throws<ArgumentNullException>(action)
                .Should()
                .NotBeNull();
        }

        [Fact]
        public async Task Providing_a_null_query_throws_an_exception()
        {
            var handler = new FilterPersonHandler(Fixture.Dummy);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null))
                .ConfigureAwait(false);

            exception.Should()
                .NotBeNull();
        }

        [Fact]
        public async Task Providing_an_empty_query_returns_the_expected_result()
        {
            var handler = new FilterPersonHandler(Fixture.Dummy);
            var result = await handler.Handle(new FilterPersonQuery())
                .ConfigureAwait(false);

            result.Should()
                .NotBeNull()
                .And
                .NotBeEmpty();
        }

        [Fact]
        public async Task Providing_a_valid_query_returns_the_expected_result()
        {
            var handler = new FilterPersonHandler(Fixture.Dummy);
            var result = await handler.Handle(new FilterPersonQuery() { Firstname = "Firstname", Lastname = "LAstname" })
                .ConfigureAwait(false);

            result.Should()
                .NotBeNull()
                .And
                .NotBeEmpty();
        }
    }
}
