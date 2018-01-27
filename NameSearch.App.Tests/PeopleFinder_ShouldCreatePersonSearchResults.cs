using Microsoft.AspNetCore.Mvc;
using Moq;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Tasks;
using NameSearch.Models.Domain.Api.Response;
using NameSearch.Repository;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.App.Tests
{
    /// <summary>
    /// Unit tests for PeopleFinder that Should Create Search Transactions
    /// </summary>
    public class PeopleFinder_ShouldCreatePersonSearchResults
    {
        /// <summary>
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        /// <summary>
        /// The mock find person controller
        /// </summary>
        private readonly Mock<IFindPersonController> MockFindPersonController;

        /// <summary>
        /// The mock export
        /// </summary>
        private readonly Mock<IExport> MockExport;

        /// <summary>
        /// The people finder
        /// </summary>
        private readonly IPeopleFinder PeopleFinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleFinder_ShouldCreatePersonSearchResults"/> class.
        /// </summary>
        public PeopleFinder_ShouldCreatePersonSearchResults()
        {
            MockRepository = new Mock<IEntityFrameworkRepository>();
            //Config Mock
            MockRepository.Setup(x => x.GetAll<Models.Entities.Name>(null, null, null, null)).Returns(MockData.GetTestSearchNames());
            MockRepository.Setup(x => x.GetAllAsync<Models.Entities.Name>(null, null, null, null)).Returns(Task.FromResult(MockData.GetTestSearchNames()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.PersonSearchJob>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.PersonSearchResult>()));
            MockRepository.Setup(x => x.Update(It.IsAny<Models.Entities.PersonSearchJob>()));
            MockRepository.Setup(x => x.Update(It.IsAny<Models.Entities.PersonSearchResult>()));
            MockRepository.Setup(x => x.Save());
            MockRepository.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
     
            MockFindPersonController = new Mock<IFindPersonController>();
            //Config Mock
            MockFindPersonController.Setup(x => x.GetFindPerson(It.IsAny<Models.Domain.Api.Request.Person>())).Returns((Models.Domain.Api.Request.Person p) => Task.FromResult(MockData.GetApiResponse(p)));

            MockExport = new Mock<IExport>();
            //Config Mock
            MockExport.Setup(x => x.ToTxt(It.IsAny<string>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToJson(It.IsAny<JObject>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToTxtAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            MockExport.Setup(x => x.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            this.PeopleFinder = new PeopleFinder(MockRepository.Object, MockFindPersonController.Object, MockExport.Object);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Run()
        {
            // Arrange
            var people = MockData.GetTestPeople();

            // Act
            var progress = new Progress<Models.Utility.ProgressReport>();
            var cancellationToken = new CancellationToken();
            var result = await PeopleFinder.Run(people, progress, cancellationToken);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);

            MockRepository.Verify(c => c.Create(It.IsAny<Models.Entities.PersonSearchJob>()), Times.Once);
            MockRepository.Verify(c => c.Create(It.IsAny<Models.Entities.PersonSearchResult>()), Times.Exactly(people.Count()));
            MockRepository.Verify(c => c.SaveAsync(), Times.Exactly(people.Count() + 1));
            MockFindPersonController.Verify(c => c.GetFindPerson(It.IsAny<Models.Domain.Api.Request.Person>()), Times.Exactly(people.Count()));
            MockExport.Verify(c => c.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(people.Count()));
        }
    }
}
