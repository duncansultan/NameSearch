using Moq;
using NameSearch.App.Factories;
using NameSearch.App.Helpers;
using NameSearch.App.Tests.Mocks;
using NameSearch.Models.Entities;
using NameSearch.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace NameSearch.App.Tests.Helpers
{
    /// <summary>
    /// Unit tests for PersonSearchResultHelper
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
            this.PersonSearchResultHelper = new PersonSearchResultHelper(MockRepository.Object, mapper, serializerSettings);
        }

        /// <summary>
        /// Imports the valid input create person search result.
        /// </summary>
        [Fact]
        public void Import_ValidInput_CreatePersonSearchResult()
        {
            //Arrange
            var fileName = "TestFileName.json";
            var jObject = MockDataFactory.GetExampleJObject();

            //Act
            var result = PersonSearchResultHelper.Import(fileName, jObject);

            //Assert
            Assert.IsType<PersonSearch>(result);
            Assert.NotNull(result);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearch>()), Times.Once);
            MockRepository.Verify(c => c.Save(), Times.Once);
        }

        /// <summary>
        /// Processes the valid input create person.
        /// </summary>
        [Fact]
        public void Process_ValidInput_ConvertSearchesToPeople()
        {
            //Arrange
            var personSearch = MockDataFactory.GetPersonSearch();
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            //Act
            var result = PersonSearchResultHelper.Process(personSearch);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Person>>(result);
            MockRepository.Verify(c => c.Create(It.IsAny<Person>()), Times.Exactly(result.Count()));
            MockRepository.Verify(c => c.Save(), Times.Once);
        }
    }
}