using AutoMapper;
using NameSearch.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NameSearch.App.Factories
{
    /// <summary>
    /// Person Entities Factory
    /// </summary>
    public class PersonEntitiesFactory
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper Mapper;

        /// <summary>
        /// The serializer settings
        /// </summary>
        private JsonSerializerSettings SerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonEntitiesBuilder"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        public PersonEntitiesFactory(IMapper mapper, JsonSerializerSettings serializerSettings)
        {
            this.Mapper = mapper;
            this.SerializerSettings = serializerSettings;
        }

        /// <summary>
        /// Gets the person entites from person search.
        /// </summary>
        /// <param name="personSearch">The person search.</param>
        /// <returns></returns>
        public IEnumerable<Person> Get(PersonSearch personSearch)
        {
            var findPersonResponse = JsonConvert.DeserializeObject<Models.Domain.Api.Response.FindPersonResponse>(personSearch.Data, SerializerSettings);

            var personEntities = MapFromFindPersonResponse(findPersonResponse);
            
            //Update Ids
            foreach (var personEntity in personEntities)
            {
                personEntity.PersonSearchId = personSearch.Id;
            }

            return personEntities;
        }

        /// <summary>
        /// Gets the person entities from find person response.
        /// </summary>
        /// <param name="findPersonResponse">The find person response.</param>
        /// <returns></returns>
        public IEnumerable<Person> Get(Models.Domain.Api.Response.FindPersonResponse findPersonResponse)
            => MapFromFindPersonResponse(findPersonResponse);

        /// <summary>
        /// Maps from find person response.
        /// </summary>
        /// <param name="findPersonResponse">The find person response.</param>
        /// <returns></returns>
        private IEnumerable<Person> MapFromFindPersonResponse(Models.Domain.Api.Response.FindPersonResponse findPersonResponse)
        {
            var personEntities = new List<Person>();

            foreach (var person in findPersonResponse.Person)
            {
                var personEntity = Mapper.Map<Person>(person);
                personEntities.Add(personEntity);
            }

            return personEntities;
        }
    }
}
