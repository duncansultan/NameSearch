﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Tasks;
using NameSearch.Repository;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            MockRepository = new Mock<IEntityFrameworkRepository>();
            //Config Mock
            MockRepository.Setup(x => x.GetAll<Models.Entities.SearchName>(null, null, null, null)).Returns(GetTestSearchNames());
            MockRepository.Setup(x => x.GetAllAsync<Models.Entities.SearchName>(null, null, null, null)).Returns(Task.FromResult(GetTestSearchNames()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.SearchJob>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.SearchTransaction>()));
            MockRepository.Setup(x => x.Update(It.IsAny<Models.Entities.SearchJob>()));
            MockRepository.Setup(x => x.Update(It.IsAny<Models.Entities.SearchTransaction>()));
            MockRepository.Setup(x => x.Save());
            MockRepository.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
     
            MockFindPersonController = new Mock<IFindPersonController>();
            //Config Mock
            MockFindPersonController.Setup(x => x.GetPerson(It.IsAny<Models.Domain.Api.Request.Person>())).Returns((Models.Domain.Api.Request.Person p) => Task.FromResult(GetJsonResult(p.Name, p.City)));

            MockExport = new Mock<IExport>();
            //Config Mock
            MockExport.Setup(x => x.ToJson(It.IsAny<string>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToJson(It.IsAny<JObject>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToJsonAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            MockExport.Setup(x => x.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            this.PeopleSearch = new PeopleSearch(MockRepository.Object, MockFindPersonController.Object, MockExport.Object);
        }

        [Fact]
        public async Task LoadPeople()
        {
            // Arrange
            var people = GetTestPeople();

            // Act
            var progress = new Progress<Models.Domain.Api.Request.Person>();
            var cancellationToken = new CancellationToken();
            var result = await PeopleSearch.Run(people, progress, cancellationToken);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);

            MockRepository.Verify(c => c.Create(It.IsAny<Models.Entities.SearchJob>()), Times.Once);
            MockRepository.Verify(c => c.Create(It.IsAny<Models.Entities.SearchTransaction>()), Times.Exactly(people.Count()));
            MockRepository.Verify(c => c.SaveAsync(), Times.Exactly(people.Count() + 1));
            MockFindPersonController.Verify(c => c.GetPerson(It.IsAny<Models.Domain.Api.Request.Person>()), Times.Exactly(people.Count()));
            MockExport.Verify(c => c.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(people.Count()));
        }

        private JsonResult GetJsonResult(string name, string city)
        {
            var person = new JObject
            {
                new JProperty("name", name),
                new JProperty("city", city)
            };

            var jObject = new JObject
            {
                new JProperty("count_person", "1"),
                new JProperty("warnings", ""),
                new JProperty("error", ""),
                new JProperty("person", person)
            };
            var result = new JsonResult(jObject.ToString())
            {
                StatusCode = (int)System.Net.HttpStatusCode.OK
            };
            return result;
        }

        private IEnumerable<Models.Domain.Api.Request.Person> GetTestPeople()
        {
            var people = new List<Models.Domain.Api.Request.Person>
            {
                new Models.Domain.Api.Request.Person
                {
                    Address1 = "",
                    Address2 = "",
                    City = "",
                    Country = "",
                    Name = "Mwangi",
                    State = "NC",
                    Zip = ""
                }
            };
            return people;
        }

        private IEnumerable<Models.Entities.SearchName> GetTestSearchNames()
        {
            var people = new List<Models.Entities.SearchName>
            {
                new Models.Entities.SearchName
                {
                    Id = 1,
                    Value = "Mwangi",
                    Description = "Kenya",
                    IsActive = true,
                    ModifiedDateTime = DateTime.Now,
                    SearchNameGroupId = 1,
                    CreatedDateTime = DateTime.Now
                }
            };
            return people;
        }
    }
}
