using AutoMapper;
using NameSearch.App.Factories;
using NameSearch.Extensions;
using NameSearch.Models.Domain;
using NameSearch.Models.Domain.Api.Response.Interfaces;
using NameSearch.Models.Entities;
using NameSearch.Repository.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NameSearch.App.Helpers
{
    /// <summary>
    /// Run Person Search
    /// </summary>
    public class PersonSearchResultHelper
    {
        #region Dependencies

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<PersonSearchResultHelper>();

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;

        /// <summary>
        /// The serializer settings
        /// </summary>
        private readonly JsonSerializerSettings SerializerSettings;
        #endregion Dependencies

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSearchResultHelper" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <param name="mapper">The mapper.</param>
        public PersonSearchResultHelper(IEntityFrameworkRepository repository,
            JsonSerializerSettings serializerSettings,
            IMapper mapper)
        {
            this.Repository = repository;
            this.SerializerSettings = serializerSettings;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Imports the specified j object.
        /// </summary>
        /// <param name="jObject">The j object.</param>
        /// <returns></returns>
        public PersonSearch Import(JObject jObject)
        {
            //todo map this
            var search = new Search();

            #region Create PersonSearchResult Entity

            var personSearch = PersonSearchResultFactory.Create(search, null, jObject);

            var log = logger.With("PersonSearchResult", personSearch);

            #endregion Create PersonSearchResult Entity

            #region Log Data Problems

            if (!string.IsNullOrWhiteSpace(personSearch.Warnings))
            {
                log.WarningEvent("Search", "FindPerson api result returned with warning messages");
            }
            if (!string.IsNullOrWhiteSpace(personSearch.Error))
            {
                log.ErrorEvent("Search", "FindPerson api result returned with error message");
            }
            if (string.IsNullOrWhiteSpace(personSearch.Data))
            {
                log.ErrorEvent("Search", "FindPerson api result returned with no person data"); ;
            }

            #endregion Log Data Problems

            #region Save Entity to Database

            Repository.Create(personSearch);
            Repository.Save();

            #endregion Save Entity to Database

            return personSearch;
        }

        /// <summary>
        /// Processes the specified person search result.
        /// </summary>
        /// <param name="personSearch">The person search result.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">personSearch</exception>
        public IEnumerable<Models.Entities.Person> Process(PersonSearch personSearch)
        {
            if (personSearch == null)
            {
                throw new ArgumentNullException(nameof(personSearch));
            }

            var log = logger.With("personSearch", personSearch);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            #region Deserialize JSON into Model

            IFindPersonResponse findPersonResponse;

            try
            {
                findPersonResponse = JsonConvert.DeserializeObject<Models.Domain.Api.Response.FindPersonResponse>(personSearch.Data, SerializerSettings);
            }
            catch (JsonException ex)
            {
                //Log and throw
                log.With("Data", personSearch.Data)
                    .ErrorEvent(ex, "Run", "Json Data Deserialization failed after {ms}ms", stopwatch.ElapsedMilliseconds);
                throw;
            }

            #endregion Deserialize JSON into Model

            var people = new List<Models.Entities.Person>();

            foreach (var person in findPersonResponse.Person)
            {
                #region Map Model into Entity

                var personEntity = Mapper.Map<Models.Entities.Person>(person);
                personEntity.PersonSearchId = personSearch.Id;
                people.Add(personEntity);

                log.With("Person", personEntity);

                #endregion Map Model into Entity

                #region Save Entity to Database

                Repository.Create(personEntity);

                foreach (var addressEntity in personEntity.Addresses)
                {
                    addressEntity.PersonId = personEntity.Id;
                    Repository.Create(addressEntity);
                }

                foreach (var associateEntity in personEntity.Associates)
                {
                    associateEntity.PersonId = personEntity.Id;
                    Repository.Create(associateEntity);
                }

                foreach (var phoneEntity in personEntity.Phones)
                {
                    phoneEntity.PersonId = personEntity.Id;
                    Repository.Create(phoneEntity);
                }

                Repository.Save();

                log.With("Data", personSearch.Data)
                    .InformationEvent("Run", "Created Person record after {ms}ms", stopwatch.ElapsedMilliseconds);

                #endregion Save Entity to Database
            }

            log.InformationEvent("Run", "Processing search result finished after {ms}ms", stopwatch.ElapsedMilliseconds);

            return people;
        }
    }
}