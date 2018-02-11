using Moq;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using NameSearch.Repository.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NameSearch.App.Tests.Mocks
{
    /// <summary>
    /// Mock for Repository
    /// </summary>
    public static class MockRepositoryFactory
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public static Mock<IEntityFrameworkRepository> Get()
        {
            var MockRepository = new Mock<IEntityFrameworkRepository>();
            //Config Mock
            MockRepository.Setup(x => x.Create(It.IsAny<PersonSearch>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Person>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Address>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Associate>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Phone>()));
            MockRepository.Setup(x => x.Update(It.IsAny<PersonSearch>()));

            MockRepository.Setup(x => x.Save());
            MockRepository.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            return MockRepository;
        }
    }
}