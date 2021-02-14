using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;
using GB.NetApi.Domain.Models.Interfaces.Repositories;
using Xunit;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.Persons
{
    public abstract class BasePersonControllerTest : BaseControllerTest<IPersonRepository>, IClassFixture<PersonDataFixture>
    {
        #region Properties

        protected const string Endpoint = "persons";
        protected const string Firstname = "Firstname";
        protected const string Lastname = "Lastname";
        protected const int BirthYear = 1990;
        protected const int BirthMonth = 1;
        protected const int BirthDay = 1;
        protected const int ID = 1;

        #endregion

        protected BasePersonControllerTest(PersonDataFixture fixture) : base(fixture) { }
    }
}
