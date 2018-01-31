using NameSearch.App.Factories;
using NameSearch.Models.Entities;
using System.Net;
using Xunit;

namespace NameSearch.App.Tests
{
    /// <summary>
    /// Unit tests for PersonSearchResultFactory
    /// </summary>
    public class PersonSearchResultFactoryTests
    {
        [Fact]
        public void Create_ValidInput_ReturnSearchResult()
        {
            // Arrange
            var personSearchRequestId = MockDataFactory.GetPersonSearchRequest().Id;
            var httpStatusCode = (int)HttpStatusCode.OK;
            var jObject = MockDataFactory.GetExampleJObject();

            // Act
            var result = PersonSearchResultFactory.Create(personSearchRequestId, httpStatusCode, jObject);

            // Assert
            Assert.IsType<PersonSearchResult>(result);
            Assert.NotNull(result);
            Assert.Equal(personSearchRequestId, result.PersonSearchRequestId);
            Assert.Equal(httpStatusCode, result.HttpStatusCode);
            Assert.Equal(jObject.ToString(), result.Data);
        }
    }
}