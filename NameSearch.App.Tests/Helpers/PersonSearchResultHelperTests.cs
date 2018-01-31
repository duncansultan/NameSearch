using Moq;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.App.Tests
{
    /// <summary>
    /// Unit tests for PeopleFinder that Should Create Search Transactions
    /// </summary>
    public class PersonSearchResultHelperTests
    {
        /// <summary>
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        /// <summary>
        /// The people finder
        /// </summary>
        private readonly PersonSearchResultHelper PersonSearchResultHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleSearchHelper_ShouldCreatePersonSearchResult"/> class.
        /// </summary>
        public PersonSearchResultHelperTests()
        {
            MockRepository = MockRepositoryFactory.Get();

            var serializerSettings = JsonSerializerSettingsFactory.Get();
            var mapper = MapperFactory.Get();
            this.PersonSearchResultHelper = new PersonSearchResultHelper(MockRepository.Object, serializerSettings, mapper);
        }

        [Fact]
        public async Task ProcessAsync_ValidInput_CreatePerson()
        {
            //Arrange
            var personSearchResult = MockDataFactory.GetPersonSearchResult();
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            //Act
            var result = await PersonSearchResultHelper.ProcessAsync(personSearchResult, cancellationToken);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Person>>(result);
            MockRepository.Verify(c => c.Create(It.IsAny<Person>()), Times.Exactly(result.Count()));
            MockRepository.Verify(c => c.SaveAsync(), Times.Exactly(result.Count()));
        }

        [Fact]
        public async Task ImportAsync_ValidInput_CreatePersonSearchResult()
        {
            //Arrange
            var jObject = MockDataFactory.GetExampleJObject();
            var personSearchJobId = MockDataFactory.GetPersonSearchJob().Id;

            //Act
            var result = await PersonSearchResultHelper.ImportAsync(jObject, personSearchJobId);

            //Assert
            Assert.IsType<PersonSearchResult>(result);
            Assert.NotNull(result);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearchRequest>()), Times.Once);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearchResult>()), Times.Once);
            MockRepository.Verify(c => c.Update(It.IsAny<PersonSearchRequest>()), Times.Once);
            MockRepository.Verify(c => c.SaveAsync(), Times.Exactly(3));
        }
    }
}
