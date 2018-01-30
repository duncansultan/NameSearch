using AutoMapper;
using Moq;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Services;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.App.Tests
{
    /// <summary>
    /// Unit tests for PeopleFinder that Should Create Search Transactions
    /// </summary>
    public class PeopleSearchRequestHelper_ShouldCreatePersonSearchResult
    {
        /// <summary>
        /// The mock repository
        /// </summary>
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        /// <summary>
        /// The mock find person controller
        /// </summary>
        private readonly Mock<IFindPersonController> MockFindPersonController;

        /// <summary>
        /// The mock export
        /// </summary>
        private readonly Mock<IExport> MockExport;

        /// <summary>
        /// The people finder
        /// </summary>
        private readonly PersonSearchRequestHelper PersonSearchRequestHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleSearchHelper_ShouldCreatePersonSearchResult"/> class.
        /// </summary>
        public PeopleSearchRequestHelper_ShouldCreatePersonSearchResult()
        {
            MockRepository = new Mock<IEntityFrameworkRepository>();
            //Config Mock
            MockRepository.Setup(x => x.Create(It.IsAny<PersonSearchResult>()));
            MockRepository.Setup(x => x.Update(It.IsAny<PersonSearchRequest>()));
            MockRepository.Setup(x => x.Save());
            MockRepository.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
     
            MockFindPersonController = new Mock<IFindPersonController>();
            //Config Mock
            MockFindPersonController.Setup(x => x.GetFindPerson(It.IsAny<Models.Domain.Api.Request.Person>())).Returns((Models.Domain.Api.Request.Person p) => Task.FromResult(MockData.GetApiResponse(p)));

            MockExport = new Mock<IExport>();
            //Config Mock
            MockExport.Setup(x => x.ToTxt(It.IsAny<string>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToJson(It.IsAny<JObject>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToTxtAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            MockExport.Setup(x => x.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var serializerSettings = new JsonSerializerSettings();

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

            this.PersonSearchRequestHelper = new PersonSearchRequestHelper(MockRepository.Object, MockFindPersonController.Object, serializerSettings, mapper, MockExport.Object);
        }

        [Fact]
        public async Task Search()
        {
            // Arrange
            var personSearchRequest = MockData.GetPersonSearchRequest();

            // Act
            var progress = new Progress<Models.Utility.ProgressReport>();
            var cancellationToken = new CancellationToken();
            var result = await PersonSearchRequestHelper.SearchAsync(personSearchRequest, cancellationToken);

            // Assert
            Assert.IsType<PersonSearchResult>(result);

            MockRepository.Verify(c => c.Update(It.IsAny<PersonSearchRequest>()), Times.Once);
            MockRepository.Verify(c => c.Create(It.IsAny<PersonSearchResult>()), Times.Once);
            MockRepository.Verify(c => c.SaveAsync(), Times.Exactly(2));
            MockFindPersonController.Verify(c => c.GetFindPerson(It.IsAny<Models.Domain.Api.Request.Person>()), Times.Once);
            MockExport.Verify(c => c.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
