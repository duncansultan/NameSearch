using AutoMapper;
using NameSearch.App.Factories;
using NameSearch.Repository;
using Serilog;
using System.Collections.Generic;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Person Helper
    /// </summary>
    public class PersonHelper
    {
        #region Dependencies

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PersonHelper>();

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
        /// Initializes a new instance of the <see cref="PersonHelper"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public PersonHelper(IEntityFrameworkRepository repository,
            IMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Gets the people.
        /// </summary>
        /// <param name="searchJobId">The search job identifier.</param>
        /// <returns></returns>
        public IEnumerable<Models.Domain.Person> GetPeople()
        {
            var peopleEntities = Repository.GetAll<Models.Entities.Person>(includeProperties: "Addresses, Phones, Associates");

            var people = new List<Models.Domain.Person>();

            foreach (var personEntity in peopleEntities)
            {
                var peopleConversion = PeopleFactory.Get(personEntity);
                people.AddRange(peopleConversion);
            }

            return people;
        }
    }
}