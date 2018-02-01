using NameSearch.Repository.Interfaces;

namespace NameSearch.Repository.Tests.Mocks
{
    /// <summary>
    /// Loads Database with Initialization Data for Unit Tests
    /// </summary>
    public static class InitialDataBuilder
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public static void Build(IEntityFrameworkRepository repository)
        {
            var nameImport = MockDataFactory.GetNameImport();
            repository.Create(nameImport);
            repository.Save();

            var names = MockDataFactory.GetNames(nameImport.Id);
            foreach (var name in names)
            {
                repository.Create(name);
                repository.Save();
            }

            var personSearchJob = MockDataFactory.GetPersonSearchJob();
            repository.Create(personSearchJob);
            repository.Save();

            var personSearchRequest = MockDataFactory.GetPersonSearchRequest(personSearchJob.Id);
            repository.Create(personSearchRequest);
            repository.Save();

            var personSearchResult = MockDataFactory.GetPersonSearchResult(personSearchRequest.Id);
            repository.Create(personSearchResult);
            repository.Save();

            var person = MockDataFactory.GetPerson(personSearchResult.Id);
            repository.Create(person);
            repository.Save();

            var addresses = MockDataFactory.GetAddresses(person.Id);
            foreach (var address in addresses)
            {
                repository.Create(address);
                repository.Save();
            }

            var associates = MockDataFactory.GetAssociates(person.Id);
            foreach (var associate in associates)
            {
                repository.Create(associate);
                repository.Save();
            }

            var phones = MockDataFactory.GetPhones(person.Id);
            foreach (var phone in phones)
            {
                repository.Create(phone);
                repository.Save();
            }
        }
    }
}