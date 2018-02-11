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
            var personSearch = MockDataFactory.GetPersonSearch();
            repository.Create(personSearch);
            repository.Save();

            var person = MockDataFactory.GetPerson(personSearch.Id);
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