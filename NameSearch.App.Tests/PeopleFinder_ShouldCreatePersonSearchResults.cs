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
    /// <summary>
    /// Unit tests for PeopleFinder that Should Create Search Transactions
    /// </summary>
    public class PeopleFinder_ShouldCreatePersonSearchResults
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
        private readonly PeopleFinder PeopleFinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleFinder_ShouldCreatePersonSearchResults"/> class.
        /// </summary>
        public PeopleFinder_ShouldCreatePersonSearchResults()
        {
            MockRepository = new Mock<IEntityFrameworkRepository>();
            //Config Mock
            MockRepository.Setup(x => x.GetAll<Models.Entities.Name>(null, null, null, null)).Returns(GetTestSearchNames());
            MockRepository.Setup(x => x.GetAllAsync<Models.Entities.Name>(null, null, null, null)).Returns(Task.FromResult(GetTestSearchNames()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.PersonSearchJob>()));
            MockRepository.Setup(x => x.Create(It.IsAny<Models.Entities.PersonSearchResult>()));
            MockRepository.Setup(x => x.Update(It.IsAny<Models.Entities.PersonSearchJob>()));
            MockRepository.Setup(x => x.Update(It.IsAny<Models.Entities.PersonSearchResult>()));
            MockRepository.Setup(x => x.Save());
            MockRepository.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
     
            MockFindPersonController = new Mock<IFindPersonController>();
            //Config Mock
            MockFindPersonController.Setup(x => x.GetFindPerson(It.IsAny<Models.Domain.Api.Request.Person>())).Returns((Models.Domain.Api.Request.Person p) => Task.FromResult(GetJsonResult(p.Name, p.City)));

            MockExport = new Mock<IExport>();
            //Config Mock
            MockExport.Setup(x => x.ToTxt(It.IsAny<string>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToJson(It.IsAny<JObject>(), It.IsAny<string>()));
            MockExport.Setup(x => x.ToTxtAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            MockExport.Setup(x => x.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            this.PeopleFinder = new PeopleFinder(MockRepository.Object, MockFindPersonController.Object, MockExport.Object);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Run()
        {
            // Arrange
            var people = GetTestPeople();

            // Act
            var progress = new Progress<Models.Domain.Api.Request.Person>();
            var cancellationToken = new CancellationToken();
            var result = await PeopleFinder.Run(people, progress, cancellationToken);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);

            MockRepository.Verify(c => c.Create(It.IsAny<Models.Entities.PersonSearchJob>()), Times.Once);
            MockRepository.Verify(c => c.Create(It.IsAny<Models.Entities.PersonSearchResult>()), Times.Exactly(people.Count()));
            MockRepository.Verify(c => c.SaveAsync(), Times.Exactly(people.Count() + 1));
            MockFindPersonController.Verify(c => c.GetFindPerson(It.IsAny<Models.Domain.Api.Request.Person>()), Times.Exactly(people.Count()));
            MockExport.Verify(c => c.ToJsonAsync(It.IsAny<JObject>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(people.Count()));
        }

        #region Mock Data

        /// <summary>
        /// Gets the json result.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="city">The city.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the test people.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the test search names.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Models.Entities.Name> GetTestSearchNames()
        {
            var people = new List<Models.Entities.Name>
            {
                new Models.Entities.Name
                {
                    Id = 1,
                    Value = "Mwangi",
                    Description = "Kenya",
                    IsActive = true,
                    ModifiedDateTime = DateTime.Now,
                    NameImportId = 1,
                    CreatedDateTime = DateTime.Now
                }
            };
            return people;
        }

        #endregion
    }
}
