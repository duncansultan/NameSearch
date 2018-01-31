using AutoMapper;
using Moq;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.App.Tests.Mocks;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.App.Tests
{
    /// <summary>
    /// Unit tests for PeopleFinder that Should Create Search Transactions
    /// </summary>
    public class PersonSearchRequestHelperTests
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
        private readonly PersonSearchRequestHelper PersonSearchRequestHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleSearchHelper_ShouldCreatePersonSearchResult"/> class.
        /// </summary>
        public PersonSearchRequestHelperTests()
        {
            MockRepository = MockRepositoryFactory.Get();
            MockFindPersonController = MockFindPersonControllerFactory.Get();
            MockExport = MockExportFactory.Get();

            var serializerSettings = JsonSerializerSettingsFactory.Get();
            var mapper = MapperFactory.Get();
            this.PersonSearchRequestHelper = new PersonSearchRequestHelper(MockRepository.Object, MockFindPersonController.Object, serializerSettings, mapper, MockExport.Object);
        }

        /// <summary>
        /// Gets the valid input return person search requests.
        /// </summary>
        [Fact]
        public void Get_ValidInput_ReturnPersonSearchRequests()
        {
            // Arrange
            var personSearchJobId = MockDataFactory.GetPersonSearchJob().Id;

            // Act
            var result = PersonSearchRequestHelper.Get(personSearchJobId);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<PersonSearchRequest>>(result);
            Assert.NotNull(result);
            MockRepository.Verify(c => c.Get<PersonSearchRequest>(It.IsAny<Expression<Func<PersonSearchRequest,bool>>>(), null, null, null, null), Times.Once);
        }

        /// <summary>
        /// Searches the asynchronous valid input create person search result.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SearchAsync_ValidInput_CreatePersonSearchResult()
        {
            // Arrange
            var personSearchRequest = MockDataFactory.GetPersonSearchRequest();

            // Act
            var progress = new Progress<Models.Utility.ProgressReport>();
            var cancellationToken = new CancellationToken();
            var result = await PersonSearchRequestHelper.SearchAsync(personSearchRequest, cancellationToken);

            // Assert
            Assert.IsType<PersonSearchResult>(result);

            MockRepository.Verify(c => c.Update(It.IsAny<PersonSearchRequest>()), Times.Once);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearchResult>()), Times.Once);
            MockRepository.Verify(c => c.SaveAsync(), Times.Exactly(2));
            MockFindPersonController.Verify(c => c.GetFindPerson(It.IsAny<Models.Domain.Api.Request.Person>()), Times.Once);
            MockExport.Verify(c => c.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
