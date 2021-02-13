using GB.NetApi.Application.WebApi.IntegrationTests.DataFixtures;

namespace GB.NetApi.Application.WebApi.IntegrationTests.Controllers.Persons
{
    public abstract class BasePersonControllerTest : BaseControllerTest
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

        protected BasePersonControllerTest(FuncBaseDbContextDataFixture fixture) : base(fixture) { }
    }
}
