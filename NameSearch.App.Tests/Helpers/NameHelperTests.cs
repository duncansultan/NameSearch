using Moq;
using NameSearch.App.Factories;
using NameSearch.App.Helpers;
using NameSearch.App.Services;
using NameSearch.App.Tests.Mocks;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using NameSearch.Repository.Interfaces;
using Xunit;

namespace NameSearch.App.Tests.Helpers
{
    /// <summary>
    /// Unit tests for NameHelper
    /// </summary>
    public class NameHelperTests
    {
        /// <summary>
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        /// <summary>
        /// The people search name helper
        /// </summary>
        private readonly NameHelper NameHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameHelperTests"/> class.
        /// </summary>
        public NameHelperTests()
        {
            MockRepository = MockRepositoryFactory.Get();

            var mapper = MapperFactory.Get();
            NameHelper = new NameHelper(MockRepository.Object, mapper);
        }

        /// <summary>
        /// Imports the valid input create names.
        /// </summary>
        [Fact]
        public void Import_ValidInput_CreateNames()
        {
            // Arrange
            var names = MockDataFactory.GetTestNames();
            // Act
            var result = NameHelper.Import(names, "test-group");
            // Assert
            Assert.IsType<long>(result);
            MockRepository.Verify(c => c.Create(It.IsAny<NameImport>()), Times.Once);
            MockRepository.Verify(c => c.Create(It.IsAny<Name>()), Times.Exactly(names.Count));
            MockRepository.Verify(c => c.Save(), Times.Exactly(names.Count + 1));
        }
    }
}