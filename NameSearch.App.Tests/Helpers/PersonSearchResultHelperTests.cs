﻿using Moq;
using NameSearch.App.Factories;
using NameSearch.App.Helpers;
using NameSearch.App.Services;
using NameSearch.App.Tests.Mocks;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using NameSearch.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            this.PersonSearchResultHelper = new PersonSearchResultHelper(MockRepository.Object, serializerSettings, mapper);
        }

        /// <summary>
        /// Imports the valid input create person search result.
        /// </summary>
        [Fact]
        public void Import_ValidInput_CreatePersonSearchResult()
        {
            //Arrange
            var jObject = MockDataFactory.GetExampleJObject();
            var personSearchJobId = MockDataFactory.GetPersonSearchJob().Id;

            //Act
            var result = PersonSearchResultHelper.Import(jObject, personSearchJobId);

            //Assert
            Assert.IsType<PersonSearchResult>(result);
            Assert.NotNull(result);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearchRequest>()), Times.Once);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearchResult>()), Times.Once);
            MockRepository.Verify(c => c.Update(It.IsAny<PersonSearchRequest>()), Times.Once);
            MockRepository.Verify(c => c.SaveAsync(), Times.Exactly(3));
        }

        /// <summary>
        /// Processes the valid input create person.
        /// </summary>
        [Fact]
        public void Process_ValidInput_CreatePerson()
        {
            //Arrange
            var personSearchResult = MockDataFactory.GetPersonSearchResult();
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            //Act
            var result = PersonSearchResultHelper.Process(personSearchResult);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Person>>(result);
            MockRepository.Verify(c => c.Create(It.IsAny<Person>()), Times.Exactly(result.Count()));
            MockRepository.Verify(c => c.SaveAsync(), Times.Exactly(result.Count()));
        }
    }
}