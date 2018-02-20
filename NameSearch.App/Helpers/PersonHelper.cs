using NameSearch.App.Factories;
using NameSearch.App.Helpers.Interfaces;
using NameSearch.Repository.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace NameSearch.App.Helpers
{
    /// <summary>
    /// Person Helper
    /// </summary>
    public class PersonHelper : IPersonHelper
    {
        #region Dependencies

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PersonHelper>();

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;
        #endregion Dependencies

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonHelper"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PersonHelper(IEntityFrameworkRepository repository)
        {
            this.Repository = repository;
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

            var distinctPeople = people.Distinct().ToList();

            return distinctPeople;
        }
    }
}