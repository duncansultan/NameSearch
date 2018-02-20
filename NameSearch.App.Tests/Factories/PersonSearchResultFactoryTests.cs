using NameSearch.App.Factories;
using NameSearch.App.Tests.Mocks;
using NameSearch.Models.Entities;
using System.Net;
using Xunit;

namespace NameSearch.App.Tests.Factories
{
    /// <summary>
    /// Unit tests for PersonSearchResultBuilder
    /// </summary>
    public class PersonSearchResultFactoryTests
    {
        /// <summary>
        /// The person search result builder
        /// </summary>
        private readonly PersonSearchResultFactory PersonSearchResultBuilder;

        public PersonSearchResultFactoryTests()
        {
            var serializerSettings = JsonSerializerSettingsFactory.Get();
            this.PersonSearchResultBuilder = new PersonSearchResultFactory(serializerSettings);
        }

        /// <summary>
        /// Creates the valid input return search result.
        /// </summary>
        [Fact]
        public void Create_ValidInput_ReturnSearchResult()
        {
            // Arrange
            var search = MockDataFactory.GetSearch();
            var httpStatusCode = (int)HttpStatusCode.OK;
            var jObject = MockDataFactory.GetExampleJObject();

            // Act
            var result = PersonSearchResultBuilder.Create(search, httpStatusCode, jObject);

            // Assert
            Assert.IsType<PersonSearch>(result);
            Assert.NotNull(result);
            Assert.Equal(httpStatusCode, result.HttpStatusCode);
            Assert.Equal(jObject.ToString(), result.Data);
        }
    }
}