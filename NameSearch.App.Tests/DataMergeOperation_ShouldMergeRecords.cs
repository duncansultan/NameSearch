using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using NameSearch.Context;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Newtonsoft.Json;
using Xunit;

namespace NameSearch.App.Tests
{
    public class DataMergeOperation_ShouldMergeRecords
    {
        private readonly IEntityFrameworkRepository Repository;
        private readonly IMapper Mapper;
        private readonly JsonSerializerSettings SerializerSettings;
        private readonly DataMergeOperation DataMergeOperation;

        public DataMergeOperation_ShouldMergeRecords()
        {
            this.Repository = new EntityFrameworkRepository(new ApplicationDbContext());
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            this.Mapper = new Mapper(new MapperConfiguration(mapperConfigurationExpression));
            this.SerializerSettings = new JsonSerializerSettings();
            this.DataMergeOperation = new DataMergeOperation(Repository, Mapper, SerializerSettings);
        }

        [Fact]
        public void MergePeople()
        {
            var searchJob = Repository.GetFirst<SearchJob>(x => !x.IsFinished);

            DataMergeOperation.MergePeople(searchJob.Id);
        }
    }
}
