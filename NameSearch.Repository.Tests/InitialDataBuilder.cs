namespace NameSearch.Repository.Tests
{
    /// <summary>
    ///     Loads Database with Initialization Data for Unit Tests
    /// </summary>
    public static class InitialDataBuilder
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        public static void Build(IEntityFrameworkRepository repository)
        {
            var nameImport = MockData.GetNameImport();
            repository.Create(nameImport);
            repository.Save();

            var names = MockData.GetNames(nameImport.Id);
            foreach (var name in names)
            {
                repository.Create(name);
                repository.Save();
            }

            var personSearchJob = MockData.GetPersonSearchJob();
            repository.Create(personSearchJob);
            repository.Save();

            var personSearchResult = MockData.GetPersonSearchResult(personSearchJob.Id);
            repository.Create(personSearchResult);
            repository.Save();

            var person = MockData.GetPerson(personSearchResult.Id);
            repository.Create(person);
            repository.Save();

            var addresses = MockData.GetAddresses(person.Id);
            foreach (var address in addresses)
            {
                repository.Create(address);
                repository.Save();
            }

            var associates = MockData.GetAssociates(person.Id);
            foreach (var associate in associates)
            {
                repository.Create(associate);
                repository.Save();
            }

            var phones = MockData.GetPhones(person.Id);
            foreach (var phone in phones)
            {
                repository.Create(phone);
                repository.Save();
            }
        }
    }
}
