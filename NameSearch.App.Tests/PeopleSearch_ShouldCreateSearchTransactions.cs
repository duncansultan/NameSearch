using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Tasks;
using NameSearch.Repository;
using NameSearch.Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.Api.Tests
{
    public class PeopleSearch_ShouldCreateSearchTransactions
    {
        private readonly Mock<IEntityFrameworkRepository> MockRepository;

        private readonly Mock<IFindPersonController> MockFindPersonController;

        private readonly Mock<IExport> MockExport;

        private readonly PeopleSearch PeopleSearch;

        public PeopleSearch_ShouldCreateSearchTransactions()
        {
            this.MockRepository = new Mock<IEntityFrameworkRepository>();
            this.MockFindPersonController = new Mock<IFindPersonController>();
            this.MockExport = new Mock<IExport>();
            this.PeopleSearch = new PeopleSearch(MockRepository.Object, MockFindPersonController.Object, MockExport.Object);
        }

        [Fact]
        public async Task LoadPeople()
        {
            // Arrange
            //ToDo: Setup Mocks
            //this.MockExport.Setup(x => x.ToJson())
            var people = GetTestPeople();

            // Act
            IProgress<Models.Domain.Api.Request.Person> progress = new Progress<Models.Domain.Api.Request.Person>();
            var result = await PeopleSearch.Run(people, progress);

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }

        private List<Models.Domain.Api.Request.Person> GetTestPeople()
        {
            var people = new List<Models.Domain.Api.Request.Person>();
            people.Add(new Models.Domain.Api.Request.Person
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
