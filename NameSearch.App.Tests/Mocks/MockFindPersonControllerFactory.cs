using Moq;
using NameSearch.Api.Controllers.Interfaces;
using System.Threading.Tasks;

namespace NameSearch.App.Tests.Mocks
{
    /// <summary>
    /// Mock for FindPersonController
    /// </summary>
    public static class MockFindPersonControllerFactory
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public static Mock<IFindPersonController> Get()
        {
            var MockFindPersonController = new Mock<IFindPersonController>();
            //Config Mock
            MockFindPersonController.Setup(x => x.GetFindPerson(It.IsAny<Models.Domain.Api.Request.Person>())).Returns((Models.Domain.Api.Request.Person p) => Task.FromResult(MockDataFactory.GetApiResponse(p)));
            return MockFindPersonController;
        }
    }
}
