using Moq;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NameSearch.App.Tests
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
            MockRepository.Setup(x => x.GetFirst<NameImport>(null, It.IsAny<Func<System.Linq.IQueryable<NameImport>, System.Linq.IOrderedQueryable<NameImport>>>(), null)).Returns(MockDataFactory.GetNameImport(It.IsAny<long>()));
            MockRepository.Setup(x => x.GetFirst<PersonSearchJob>(null, null, null)).Returns(MockDataFactory.GetPersonSearchJob());
            MockRepository.Setup(x => x.GetFirst<PersonSearchJob>(It.IsAny<Expression<Func<PersonSearchJob, bool>>>(), null, null)).Returns(MockDataFactory.GetPersonSearchJob());
            MockRepository.Setup(x => x.GetFirst<PersonSearchJob>(It.IsAny<Expression<Func<PersonSearchJob, bool>>>(), null, It.IsAny<string>())).Returns(MockDataFactory.GetPersonSearchJob());
            MockRepository.Setup(x => x.GetFirstAsync<PersonSearchJob>(null, null, null)).Returns(Task.FromResult(MockDataFactory.GetPersonSearchJob()));
            MockRepository.Setup(x => x.GetFirstAsync<PersonSearchJob>(It.IsAny<Expression<Func<PersonSearchJob, bool>>>(), null, null)).Returns(Task.FromResult(MockDataFactory.GetPersonSearchJob()));
            MockRepository.Setup(x => x.GetFirstAsync<PersonSearchJob>(It.IsAny<Expression<Func<PersonSearchJob, bool>>>(), null, It.IsAny<string>())).Returns(Task.FromResult(MockDataFactory.GetPersonSearchJob()));

            MockRepository.Setup(x => x.Create(It.IsAny<PersonSearchJob>()));
            MockRepository.Setup(x => x.Create(It.IsAny<PersonSearchRequest>()));
            MockRepository.Setup(x => x.Create(It.IsAny<PersonSearchResult>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Person>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Address>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Associate>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Phone>()));

            MockRepository.Setup(x => x.Update(It.IsAny<PersonSearchJob>()));
            MockRepository.Setup(x => x.Update(It.IsAny<PersonSearchRequest>()));
            MockRepository.Setup(x => x.Update(It.IsAny<PersonSearchResult>()));

            MockRepository.Setup(x => x.Save());
            MockRepository.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            return MockRepository;
        }
    }
}