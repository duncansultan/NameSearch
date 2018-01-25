using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using NameSearch.Context;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Newtonsoft.Json;
using Xunit;
using NameSearch.App.Tasks;
using Moq;
using System.Threading.Tasks;

namespace NameSearch.App.Tests
{
    public class PeopleSearchResultProcessor_ShouldConvertPersonSearchResultsToPeople
    {
        private readonly Mock<IEntityFrameworkRepository> MockRepository;
        private readonly IMapper Mapper;
        private readonly JsonSerializerSettings SerializerSettings;
        private readonly PeopleSearchResultProcessor PeopleMerge;

        public PeopleSearchResultProcessor_ShouldConvertPersonSearchResultsToPeople()
        {
            this.MockRepository = new Mock<IEntityFrameworkRepository>();
            //Config Mock
            MockRepository.Setup(x => x.GetFirst<Models.Entities.PersonSearchJob>(null, null, null)).Returns(MockData.GetPersonSearchJob());
            MockRepository.Setup(x => x.GetFirstAsync<Models.Entities.PersonSearchJob>(null, null, null)).Returns(Task.FromResult(MockData.GetPersonSearchJob()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.Person>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.Address>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.Associate>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.Phone>()));
            MockRepository.Setup(x => x.Update(It.IsAny<Models.Entities.PersonSearchJob>()));
            MockRepository.Setup(x => x.Save());
            MockRepository.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            //ToDo: Add this to DI container
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Domain.Api.Response.Person, Models.Entities.Person>();
            });
            Mapper = new Mapper(config);

            this.SerializerSettings = new JsonSerializerSettings(); 
            this.PeopleMerge = new PeopleSearchResultProcessor(MockRepository.Object, Mapper, SerializerSettings);
        }

        [Fact]
        public async Task  MergePeople()
        {
            //Arrange
            //ToDo: Mock a SearchJob with Multiple Transactions
            var searchJob = MockRepository.Object.GetFirst<PersonSearchJob>();
            var result = await PeopleMerge.Run(searchJob);
            //Act

            //Assert
            Assert.True(result);
            //ToDo: Get People and verify that they are in the Search Job Transactions
            //ToDo: Verify that searchJob IsComplete flag
        }
    }
}
