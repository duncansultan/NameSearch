﻿using AutoMapper;
using Moq;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.App.Tests.Mocks;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
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
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        /// <summary>
        /// The people search
        /// </summary>
        private readonly PeopleSearch PeopleSearch;

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
            this.MockExport = MockExportFactory.Get();
            var resultOutputPath = "";
            var searchWaitMs = 60000;

            this.PeopleSearch = new PeopleSearch(MockRepository.Object, MockFindPersonController.Object, SerializerSettings, Mapper, MockExport.Object, resultOutputPath, searchWaitMs);
        }

        /// <summary>
        /// Searches the asynchronous valid input return success.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SearchAsync_ValidInput_ReturnSuccess()
        {
            // Arrange
            var searchCriteria = MockDataFactory.GetTestSearchCriteria();
            var names = MockDataFactory.GetNames();
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            // Act
            var result = await PeopleSearch.SearchAsync(searchCriteria, names, cancellationToken);
            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
            //ToDo verify Mocks
        }
    }
}