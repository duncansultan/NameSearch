using AutoMapper;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Newtonsoft.Json;

namespace NameSearch.App
{
    public class DataMergeOperation
    {
        private readonly IEntityFrameworkRepository Repository;
        private readonly IMapper Mapper;
        private readonly JsonSerializerSettings SerializerSettings;

        public DataMergeOperation(IEntityFrameworkRepository repository, IMapper mapper, JsonSerializerSettings serializerSettings)
        {
            this.Repository = repository;
            this.Mapper = mapper;
            this.SerializerSettings = serializerSettings;
        }

        public void MergePeople(int searchJobId)
        {
            var searchJob = Repository.GetFirst<SearchJob>(x => x.Id == searchJobId, null, "Searches");

            foreach (var search in searchJob.Searches)
            {
                var person = JsonConvert.DeserializeObject<Models.Domain.Person>(search.Json);
                var personEntity = Mapper.Map<Person>(person);

                //ToDo: Figure out how to detect if record is new or changed.  Maybe add a merge function to the Repository?
                //Repository.GetExists<Person>(x => x.LastName == )

                Repository.Create(personEntity);
                Repository.Save();
            }
        }
    }
}
