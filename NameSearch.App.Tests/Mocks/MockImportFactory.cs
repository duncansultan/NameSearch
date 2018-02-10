using Moq;
using NameSearch.Utility.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Tests.Mocks
{
    /// <summary>
    /// Mock for Import Utility
    /// </summary>
    public static class MockImportFactory
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public static Mock<IImport> Get()
        {
            var MockImport = new Mock<IImport>();
            MockImport.Setup(x => x.FromJson(It.IsAny<string>())).Returns(MockDataFactory.GetExampleJObject());
            MockImport.Setup(x => x.FromJsonAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(MockDataFactory.GetExampleJObject()));
            return MockImport;
        }
    }
}