using AutoMapper;
using Moq;
using NameSearch.App.Services;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.App.Tests
{
    /// <summary>
    /// Unit tests for PeopleFinder that Should Create Search Transactions
    /// </summary>
    public class PeopleSearchHelper_ShouldProcessPersonSearchResultAndCreatePerson
    {
        /// <summary>
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        /// <summary>
        /// The people finder
        /// </summary>
        private readonly PersonSearchResultHelper PersonSearchResultHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleSearchHelper_ShouldCreatePersonSearchResult"/> class.
        /// </summary>
        public PeopleSearchHelper_ShouldProcessPersonSearchResultAndCreatePerson()
        {
            MockRepository = new Mock<IEntityFrameworkRepository>();
            //Config Mock
            MockRepository.Setup(x => x.Create(It.IsAny<PersonSearchResult>()));
            MockRepository.Setup(x => x.Update(It.IsAny<PersonSearchRequest>()));
            MockRepository.Setup(x => x.Save());
            MockRepository.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            MockRepository.Setup(x => x.GetFirst<PersonSearchJob>(null, null, null)).Returns(MockData.GetPersonSearchJob());
            MockRepository.Setup(x => x.GetFirstAsync<PersonSearchJob>(null, null, null)).Returns(Task.FromResult(MockData.GetPersonSearchJob()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.Person>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.Address>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.Associate>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.Phone>()));
            MockRepository.Setup(x => x.Update(It.IsAny<PersonSearchJob>()));
            MockRepository.Setup(x => x.Save());
            MockRepository.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var serializerSettings = new JsonSerializerSettings();

            //Unmapped properties:
            //PersonSearchResultId
            //FirstName
            //LastName
            //Alias
            //Age
            //Addresses
            //Phones
            //Associates
            //Id
            //IsActive
            //CreatedDateTime
            //ModifiedDateTime

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Domain.Api.Response.Person, Models.Entities.Person>()
                    .ForMember(x => x.Addresses,
                               m => m.MapFrom(a => a.CurrentAddresses))
                    .ForMember(x => x.Associates,
                               m => m.MapFrom(a => a.AssociatedPeople))
                    .ForMember(x => x.Phones,
                               m => m.MapFrom(a => a.Phones)).ReverseMap();
            });
            var mapper = new Mapper(config);

            this.PersonSearchResultHelper = new PersonSearchResultHelper(MockRepository.Object, serializerSettings, mapper);
        }

        [Fact]
        public async Task Process()
        {

        }
    }
}
