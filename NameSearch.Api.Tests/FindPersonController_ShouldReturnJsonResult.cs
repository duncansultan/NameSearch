using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NameSearch.Api.Controllers;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.Models.Domain.Api.Request;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.Api.Tests
{
    public class FindPersonController_ShouldReturnJsonResult
    {
        private readonly IFindPersonController Controller;

        public FindPersonController_ShouldReturnJsonResult()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var serializerSettings = new JsonSerializerSettings();
            this.Controller = new FindPersonController(configuration, serializerSettings);
        }

#pragma warning disable xUnit1004 // Test methods should not be skipped
        [Fact(Skip = "Integration test incurs financial expense and should be manually initiated.")]
#pragma warning restore xUnit1004 // Test methods should not be skipped
        public async Task GetPerson()
        {
            //Arrange
            var model = GetTestPerson();

            //Act
            var response = await Controller.GetFindPerson(model);

            //Assert
            Assert.NotNull(response);
            Assert.IsAssignableFrom<JsonResult>(response);
            Assert.NotNull(response.SerializerSettings);
            Assert.NotNull(response.Value);
            Assert.IsAssignableFrom<string>(response.Value);
            Assert.False(string.IsNullOrWhiteSpace(response.Value.ToString()), "Response string is empty null or whitespace.");
        }

        private Person GetTestPerson()
        {
            var person = new Person
            {
                Address1 = "",
                Address2 = "",
                City = "",
                Country = "",
                Name = "Mwangi",
                State = "NC",
                Zip = ""
            };
            return person;
        }
    }
}
