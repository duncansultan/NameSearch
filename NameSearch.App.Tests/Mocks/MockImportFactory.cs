using Moq;
using NameSearch.Utility.Interfaces;
using System.Threading;

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
            MockImport.Setup(x => x.FromCsv<string>(It.IsAny<string>())).Returns(MockDataFactory.GetTestNames());
            MockImport.Setup(x => x.FromJson(It.IsAny<string>()));
            MockImport.Setup(x => x.FromJsonAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()));
            return MockImport;
        }
    }
}