using AutoMapper;
using NameSearch.App.Factories;
using NameSearch.App.Helpers.Interfaces;
using NameSearch.Extensions;
using NameSearch.Models.Entities;
using NameSearch.Repository.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NameSearch.App.Helpers
{
    /// <summary>
    /// Person Search Result Helper
    /// </summary>
    public class PersonSearchResultHelper : IPersonSearchResultHelper
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
        /// The person entities factory
        /// </summary>
        private readonly PersonEntitiesFactory PersonEntitiesFactory;

        /// <summary>
        /// The person search result factory
        /// </summary>
        private readonly PersonSearchResultFactory PersonSearchResultFactory;

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
        /// <param name="mapper">The mapper.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        public PersonSearchResultHelper(IEntityFrameworkRepository repository,
            IMapper mapper,
            JsonSerializerSettings serializerSettings)
        {
            this.Repository = repository;
            this.SerializerSettings = serializerSettings;
            this.Mapper = mapper;
            this.PersonEntitiesFactory = new PersonEntitiesFactory(mapper, serializerSettings);
            this.PersonSearchResultFactory = new PersonSearchResultFactory(serializerSettings);
        }

        /// <summary>
        /// Imports the specified j object.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="jObject">The j object.</param>
        /// <returns></returns>
        public PersonSearch Import(string fileName, JObject jObject)
        {
            var log = logger.With("fileName", fileName);

            #region Create PersonSearchResult Entity

            var personSearch = PersonSearchResultFactory.Create(fileName, jObject);

            log.With("PersonSearchResult", personSearch);

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
        public IEnumerable<Person> Process(PersonSearch personSearch)
        {
            if (personSearch == null)
            {
                throw new ArgumentNullException(nameof(personSearch));
            }

            var stopwatch = new Stopwatch();

            var log = logger.With("personSearch", personSearch);

            stopwatch.Start();

            var personEntities = PersonEntitiesFactory.Get(personSearch);

            log.DebugEvent("Process", "Converted PersonSearch to PersonEntities after {ms}ms", stopwatch.ElapsedMilliseconds);

            SavePeople(personEntities);

            stopwatch.Stop();

            log.InformationEvent("Process", "Processing search result finished after {ms}ms", stopwatch.ElapsedMilliseconds);

            return personEntities;
        }

        /// <summary>
        /// Saves the people.
        /// </summary>
        /// <param name="personEntities">The person entities.</param>
        /// <exception cref="ArgumentNullException">personEntities</exception>
        private void SavePeople(IEnumerable<Person> personEntities)
        {
            if (personEntities == null)
            {
                throw new ArgumentNullException(nameof(personEntities));
            }

            foreach (var personEntity in personEntities)
            {
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

                logger.DebugEvent("SavePeople", "Created Person record for {lastName}, {firstName} after {ms}ms", personEntity.LastName, personEntity.FirstName);
            }

            Repository.Save();
        }
    }
}