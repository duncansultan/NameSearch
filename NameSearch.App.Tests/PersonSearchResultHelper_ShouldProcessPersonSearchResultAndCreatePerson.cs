using AutoMapper;
using Moq;
using NameSearch.App.Services;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Newtonsoft.Json;
using System.Threading;
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

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Domain.Api.Response.Person, Models.Entities.Person>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.PersonSearchResultId, opt => opt.Ignore())
                    .ForMember(x => x.IsActive, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.ModifiedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.Alias, opt => opt.Ignore())
                    .ForMember(x => x.AgeRange, m => m.MapFrom(a => a.AgeRange))
                    .ForMember(x => x.AgeRange, m => m.MapFrom(a => a.AgeRange))
                    .ForMember(x => x.Addresses, m => m.MapFrom(a => a.CurrentAddresses))
                    .ForMember(x => x.Associates, m => m.MapFrom(a => a.AssociatedPeople))
                    .ForMember(x => x.Phones, m => m.MapFrom(a => a.Phones))
                    .ReverseMap();
                cfg.CreateMap<Models.Domain.Api.Response.Address, Models.Entities.Address>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.PersonId, opt => opt.Ignore())
                    .ForMember(x => x.IsActive, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.ModifiedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.Address1, m => m.MapFrom(a => a.StreetLine1))
                    .ForMember(x => x.Address2, m => m.MapFrom(a => a.StreetLine2))
                    .ForMember(x => x.City, m => m.MapFrom(a => a.City))
                    .ForMember(x => x.State, m => m.MapFrom(a => a.StateCode))
                    .ForMember(x => x.Zip, m => m.MapFrom(a => a.PostalCode))
                    .ForMember(x => x.Plus4, m => m.MapFrom(a => a.Zip4))
                    .ForMember(x => x.Country, m => m.MapFrom(a => a.CountryCode))
                    .ReverseMap();
                cfg.CreateMap<Models.Domain.Api.Response.Associate, Models.Entities.Associate>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.PersonId, opt => opt.Ignore())
                    .ForMember(x => x.IsActive, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.ModifiedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.Name, m => m.MapFrom(a => a.Name))
                    .ForMember(x => x.Relation, m => m.MapFrom(a => a.Relation))
                    .ReverseMap();
                cfg.CreateMap<Models.Domain.Api.Response.Phone, Models.Entities.Phone>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.PersonId, opt => opt.Ignore())
                    .ForMember(x => x.IsActive, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.ModifiedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.PhoneNumber, m => m.MapFrom(a => a.PhoneNumber))
                    .ReverseMap();
            });
            var mapper = new Mapper(config);


            this.PersonSearchResultHelper = new PersonSearchResultHelper(MockRepository.Object, serializerSettings, mapper);
        }

        [Fact]
        public async Task Process()
        {
            //Arrange
            var personSearchResult = MockData.GetPersonSearchResult();
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            //Act
            var result = await PersonSearchResultHelper.ProcessAsync(personSearchResult, cancellationToken);

            //Assert
            Assert.IsType<int>(result);
            //ToDo Add all the mock data checks here for 100 records
        }
    }
}
