using Moq;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.App.Tests.Mocks
{
    /// <summary>
    /// Mock for Export Utility
    /// </summary>
    public static class MockExportFactory
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public static Mock<IExport> Get()
        {
            var MockExport = new Mock<IExport>();
            MockExport.Setup(x => x.ToTxt(It.IsAny<string>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToJson(It.IsAny<JObject>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToTxtAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            MockExport.Setup(x => x.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            return MockExport;
        }
    }
}