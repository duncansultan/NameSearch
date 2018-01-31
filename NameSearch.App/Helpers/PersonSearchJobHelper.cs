using AutoMapper;
using NameSearch.Models.Domain;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Serilog;
using System.Collections.Generic;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Create Search Jobs and Requests
    /// </summary>
    public class PersonSearchJobHelper
    {
        #region Dependencies

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PersonSearchJobHelper>();

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;
        #endregion Dependencies

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSearchJobHelper"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public PersonSearchJobHelper(IEntityFrameworkRepository repository,
            IMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Completes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="isSuccessful">if set to <c>true</c> [is successful].</param>
        public void Complete(long id, bool isSuccessful = true)
        {
            var personSearchJob = Repository.GetFirst<PersonSearchJob>(x => x.Id == id);
            personSearchJob.IsProcessed = true;
            personSearchJob.IsSuccessful = isSuccessful;
            Repository.Update(personSearchJob);
            Repository.Save();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public long Create()
        {
            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();
            return personSearchJob.Id;
        }

        /// <summary>
        /// Creates the search job.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="nameImportId">The name import identifier.</param>
        /// <returns></returns>
        public long CreateWithRequests(SearchCriteria searchCriteria, IEnumerable<string> names)
        {
            var personSearchJob = new PersonSearchJob();
            Repository.Create(personSearchJob);
            Repository.Save();

            foreach (var name in names)
            {
                var personSearchRequest = new PersonSearchRequest()
                {
                    PersonSearchJobId = personSearchJob.Id,
                    Name = name,
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
    }
}