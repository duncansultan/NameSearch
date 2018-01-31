using System.Collections.Generic;
using System.Linq;

namespace NameSearch.App.Factories
{
    /// <summary>
    /// Factory to Create Person Models from Person Entity with multiple Address records
    /// </summary>
    public static class PeopleFactory
    {
        /// <summary>
        /// Gets the specified person entity.
        /// </summary>
        /// <param name="personEntity">The person entity.</param>
        /// <returns></returns>
        public static IEnumerable<Models.Domain.Person> Get(Models.Entities.Person personEntity)
        {
            var people = new List<Models.Domain.Person>();

            foreach (var address in personEntity.Addresses)
            {
                var person = Initialize(personEntity);
                person.Address1 = address.Address1;
                person.Address2 = address.Address2;
                person.City = address.City;
                person.State = address.State;
                person.Zip = address.Zip;
                person.Plus4 = address.Plus4;
                person.Country = address.Country;
                person.Latitude = address.Latitude;
                person.Longitude = address.Longitude;
                people.Add(person);
            }

            return people;
        }

        /// <summary>
        /// Initializes the specified person entity.
        /// </summary>
        /// <param name="personEntity">The person entity.</param>
        /// <returns></returns>
        private static Models.Domain.Person Initialize(Models.Entities.Person personEntity)
        {
            return new Models.Domain.Person
            {
                FirstName = personEntity.FirstName,
                LastName = personEntity.LastName,
                Phone = personEntity.Phones?.FirstOrDefault().PhoneNumber,
                AgeRange = personEntity.AgeRange
            };
        }
    }
}