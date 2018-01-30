using AutoMapper;
using NameSearch.Models.Domain;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Serilog;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Create Search Jobs and Requests
    /// </summary>
    public class PeopleSearchJobHelper
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PeopleSearchJobHelper>();
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleSearchJobHelper"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public PeopleSearchJobHelper(IEntityFrameworkRepository repository,
            IMapper mapper)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Creates the search job.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="nameImportId">The name import identifier.</param>
        /// <returns></returns>
        public long Create(SearchCriteria searchCriteria, long nameImportId)
        {
            var names = Repository.Get<Name>(x => x.NameImportId == nameImportId);

            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            foreach (var name in names)
            {
                var personSearchRequest = new PersonSearchRequest()
                {
                    PersonSearchJobId = personSearchJob.Id,
                    Name = name.Value,
                    Address1 = searchCriteria.Address1,
                    Address2 = searchCriteria.Address2,
                    City = searchCriteria.City,
                    State = searchCriteria.State,
                    Zip = searchCriteria.Zip,
                    Country = searchCriteria.Country
                };
                Repository.Create(personSearchRequest);
                Repository.Save();
            }

            return personSearchJob.Id;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PersonSearchJob Get(long id)
        {
            var personSearchJob = Repository.GetFirst<PersonSearchJob>(x => x.Id == id, includeProperties: "PersonSearchRequests");
            return personSearchJob;
        }

        /// <summary>
        /// Finishes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Complete(long id)
        {
            var personSearchJob = Repository.GetFirst<PersonSearchJob>(x => x.Id == id);
            personSearchJob.IsProcessed = true;
            personSearchJob.IsSuccessful = true;
            Repository.Update(personSearchJob);
            Repository.Save();
        }
    }
}
