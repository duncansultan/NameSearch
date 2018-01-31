using Moq;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using System;
using System.Linq.Expressions;
using Xunit;

namespace NameSearch.App.Tests
{
    public class PersonSearchJobHelperTests
    {
        /// <summary>
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        /// <summary>
        /// The people search name helper
        /// </summary>

        private readonly PersonSearchJobHelper PeopleSearchJobHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSearchJobHelperTests"/> class.
        /// </summary>
        public PersonSearchJobHelperTests()
        {
            MockRepository = MockRepositoryFactory.Get();

            var mapper = MapperFactory.Get();
            PeopleSearchJobHelper = new PersonSearchJobHelper(MockRepository.Object, mapper);
        }

        [Fact]
        public void Get_ValidInput_ReturnPersonSearchJob()
        {
            // Arrange
            var id = MockDataFactory.GetPersonSearchJob().Id;
            // Act
            var result = PeopleSearchJobHelper.Get(id);
            // Assert
            Assert.IsType<PersonSearchJob>(result);
            Assert.NotNull(result);
            MockRepository.Verify(c => c.GetFirst<PersonSearchJob>(It.IsAny<Expression<Func<PersonSearchJob, bool>>>(), null, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Create_NoInput_ReturnPersonSearchJob()
        {
            // Arrange

            // Act
            var result = PeopleSearchJobHelper.Create();
            // Assert
            Assert.IsType<long>(result);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearchJob>()), Times.Once);
            MockRepository.Verify(c => c.Save(), Times.Once);
        }

        [Fact]
        public void Create_ValidInput_ReturnPersonSearchJob()
        {
            // Arrange
            var criteria = MockDataFactory.GetTestSearchCriteria();
            var names = MockDataFactory.GetTestNames();
            // Act
            var result = PeopleSearchJobHelper.CreateWithRequests(criteria, names);
            // Assert
            Assert.IsType<long>(result);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearchJob>()), Times.Once);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearchJob>()), Times.Exactly(names.Count));
            MockRepository.Verify(c => c.Save(), Times.Exactly(names.Count + 1));            
        }

        [Fact]
        public void Complete_ValidInput_UpdatePersonSearchJob()
        {
            // Arrange
            var id = MockDataFactory.GetPersonSearchJob().Id;
            // Act
            PeopleSearchJobHelper.Complete(id, true);
            // Assert
            MockRepository.Verify(c => c.Update<PersonSearchJob>(It.IsAny<PersonSearchJob>()), Times.Once);
            MockRepository.Verify(c => c.Save(), Times.Once);
        }
    }
}
