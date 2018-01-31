using AutoMapper;
using Moq;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.App.Tasks;
using NameSearch.App.Tests.Mocks;
using NameSearch.Models.Domain;
using NameSearch.Repository;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.App.Tests.Services
{
    /// <summary>
    /// Unit tests for PeopleSearch
    /// </summary>
    public class PeopleSearchTests
    {
        #region Dependencies

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// The mock export
        /// </summary>
        private readonly Mock<IExport> MockExport;

        /// <summary>
        /// The mock find person controller
        /// </summary>
        private readonly Mock<IFindPersonController> MockFindPersonController;

        /// <summary>
        /// The mock import
        /// </summary>
        private readonly Mock<IImport> MockImport;

        /// <summary>
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;
        //ToDo setup as mock
        /// <summary>
        /// The people search name helper
        /// </summary>
        private readonly NameHelper NameHelper;

        /// <summary>
        /// The people search
        /// </summary>
        private readonly PeopleSearch PeopleSearch;
        //ToDo setup as mock
        /// <summary>
        /// The people search person helper
        /// </summary>
        private readonly PersonHelper PersonHelper;
        //ToDo setup as mock
        /// <summary>
        /// The people search job helper
        /// </summary>
        private readonly PersonSearchJobHelper PersonSearchJobHelper;
        //ToDo setup as mock
        /// <summary>
        /// The people search request helper
        /// </summary>
        private readonly PersonSearchRequestHelper PersonSearchRequestHelper;
        //ToDo setup as mock
        /// <summary>
        /// The people search result helper
        /// </summary>
        private readonly PersonSearchResultHelper PersonSearchResultHelper;

        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;

        #endregion Dependencies

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleSearchTests"/> class.
        /// </summary>
        public PeopleSearchTests()
        {
            this.MockRepository = MockRepositoryFactory.Get();
            this.MockFindPersonController = MockFindPersonControllerFactory.Get();
            this.SerializerSettings = JsonSerializerSettingsFactory.Get();
            this.Mapper = MapperFactory.Get();
            this.MockImport = MockImportFactory.Get();
            this.MockExport = MockExportFactory.Get();

            this.PersonSearchRequestHelper = new PersonSearchRequestHelper(MockRepository.Object, MockFindPersonController.Object, SerializerSettings, Mapper, MockExport.Object);
            this.PersonSearchResultHelper = new PersonSearchResultHelper(MockRepository.Object, SerializerSettings, Mapper);
            this.PersonSearchJobHelper = new PersonSearchJobHelper(MockRepository.Object, Mapper);
            this.PersonHelper = new PersonHelper(MockRepository.Object, Mapper);
            this.NameHelper = new NameHelper(MockRepository.Object, Mapper);
            this.PeopleSearch = new PeopleSearch(MockRepository.Object, MockFindPersonController.Object, SerializerSettings, Mapper, MockImport.Object, MockExport.Object);
        }

        /// <summary>
        /// Exports the people valid directory export people to CSV.
        /// </summary>
        [Fact]
        public void ExportPeople_ValidDirectory_ExportPeopleToCsv()
        {
            // Arrange
            var fileName = "Export-People-File.csv";
            // Act
            PeopleSearch.ExportPeople(fileName);
            // Assert
            //ToDo MockPersonHelper.Verify(c => c.GetPeople().Result(It.IsAny<IEnumerable<Person>()));
            MockExport.Verify(c => c.ToCsv<Person>(It.IsAny<IEnumerable<Person>>(), It.Is<string>(x => x == fileName), It.IsAny<bool>()), Times.Once);
        }

        /// <summary>
        /// Imports the names valid file create name records.
        /// </summary>
        [Fact]
        public void ImportNames_ValidFile_CreateNameRecords()
        {
            // Arrange
            var fileName = "Import-Names-File.csv";
            // Act
            var result = PeopleSearch.ImportNames(fileName);
            // Assert
            Assert.IsType<long>(result);
            //ToDo NameHelper.Verify(c => c.Import().Result(It.IsAny<IEnumerable<Person>()));
        }

        [Fact]
        public async Task ImportPersonSearchesFromJsonAsync_ValidFile_CreatePersonSearchResult()
        {
            // Arrange
            var fileName = "Import-Person-Search.json";
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            // Act
            var result = await PeopleSearch.ImportPersonSearchesFromJsonAsync(fileName, cancellationToken);
            // Assert
            Assert.IsType<int>(result);
            Assert.True(result > 0);
            //ToDo verify Mocks
        }

        [Fact]
        public async Task SearchAsync_ValidInput_ReturnSuccess()
        {
            // Arrange
            var searchCriteria = MockDataFactory.GetTestSearchCriteria();
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            // Act
            var result = await PeopleSearch.SearchAsync(searchCriteria, cancellationToken);
            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
            //ToDo verify Mocks
        }
    }
}