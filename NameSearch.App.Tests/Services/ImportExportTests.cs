using AutoMapper;
using Moq;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.App.Tests.Mocks;
using NameSearch.Models.Domain;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.App.Tests.Services
{
    /// <summary>
    /// Unit tests for ImportExport
    /// </summary>
    public class ImportExportTests
    {
        #region Dependencies

        /// <summary>
        /// The people search
        /// </summary>
        private readonly ImportExport ImportExport;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// The mock export
        /// </summary>
        private readonly Mock<IExport> MockExport;

        /// <summary>
        /// The mock import
        /// </summary>
        private readonly Mock<IImport> MockImport;

        /// <summary>
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;

        #endregion Dependencies

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExportTests"/> class.
        /// </summary>
        public ImportExportTests()
        {
            this.MockRepository = MockRepositoryFactory.Get();
            this.SerializerSettings = JsonSerializerSettingsFactory.Get();
            this.Mapper = MapperFactory.Get();
            this.MockImport = MockImportFactory.Get();
            this.MockExport = MockExportFactory.Get();
            this.ImportExport = new ImportExport(MockRepository.Object, this.SerializerSettings, Mapper, MockImport.Object, MockExport.Object);
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
            ImportExport.ExportPeople(fileName);
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
            var result = ImportExport.ImportNames(fileName);
            // Assert
            Assert.IsType<long>(result);
            //ToDo NameHelper.Verify(c => c.Import().Result(It.IsAny<IEnumerable<Person>()));
        }

        /// <summary>
        /// Imports the person searches from json asynchronous valid file create person search result.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ImportPersonSearchesFromJsonAsync_ValidFile_CreatePersonSearchResult()
        {
            // Arrange
            var fileName = "Import-Person-Search.json";
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            // Act
            var result = await ImportExport.ImportPersonSearches(fileName, cancellationToken);
            // Assert
            Assert.IsType<int>(result);
            Assert.True(result > 0);
            //ToDo verify Mocks
        }
    }
}