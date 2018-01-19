using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App;
using NameSearch.Models.Domain;
using NameSearch.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.Api.Tests
{
    public class PeopleSearch_ShouldCreateSearchTransactions
    {
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        private readonly Mock<IFindPersonController> MockFindPersonController;

        private readonly Mock<IMapper> MockMapper;

        private readonly string OutputDirectory;

        private readonly Mock<PeopleSearch> MockSearchOperation;

        public PeopleSearch_ShouldCreateSearchTransactions()
        {
            this.MockRepository = new Mock<IEntityFrameworkRepository>();
            this.MockFindPersonController = new Mock<IFindPersonController>();
            this.MockMapper = new Mock<IMapper>();
            this.OutputDirectory = "ToDo";
            this.MockSearchOperation = new Mock<PeopleSearch>(MockRepository.Object, MockFindPersonController.Object, MockMapper.Object, OutputDirectory);
            this.MockSearchOperation.Setup(x => x.Run(GetTestPeople())).Returns(new List<Person>());
        }

        [Fact]
        public async Task LoadPeople()
        {
            // Arrange
            var people = GetTestPeople();

            // Act
            var result = await MockSearchOperation.Object.Run(people);

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }

        private List<Person> GetTestPeople()
        {
            var people = new List<Person>();
            people.Add(new Person
            {
                Address1 = "",
                Address2 = "",
                City = "",
                Country = "",
                Name = "Mwangi",
                State = "NC",
                Zip = ""
            });
            return people;
        }
    }
}
