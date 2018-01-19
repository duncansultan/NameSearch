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

namespace NameSearch.App.Tests
{
    public class PeopleMerge_ShouldMergeRecords
    {
        private readonly Mock<IEntityFrameworkRepository> MockRepository;
        private readonly Mock<IMapper> MockMapper;
        private readonly JsonSerializerSettings SerializerSettings;
        private readonly PeopleMerge PeopleMerge;

        public PeopleMerge_ShouldMergeRecords()
        {
            this.MockRepository = new Mock<IEntityFrameworkRepository>();
            this.MockMapper = new Mock<IMapper>();
            this.SerializerSettings = new JsonSerializerSettings(); 
            this.PeopleMerge = new PeopleMerge(MockRepository.Object, MockMapper.Object, SerializerSettings);
        }

        [Fact]
        public void MergePeople()
        {
            //Arrange
            //ToDo: Mock a SearchJob with Multiple Transactions
            var searchJob = MockRepository.Object.GetFirst<SearchJob>(x => !x.IsFinished);

            //Act

            //Assert
            //ToDo: Get People and verify that they are in the Search Job Transactions
            //ToDo: Verify that searchJob IsComplete flag
        }
    }
}
