using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NameSearch.Api.Controllers;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.Models.Domain;
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
            //ToDo: Consider mocking this controller.
            Controller = new FindPersonController(configuration, serializerSettings);
        }

        [Fact]
        public async Task GetPerson()
        {
            //Arrange
            IPerson model = new PersonSearch
            {
                Address1 = "",
                Address2 = "",
                City = "",
                Country = "",
                Name = "Mwangi",
                State = "NC",
                Zip = ""
            };

            //Act
            var response = await Controller.GetPerson(model);

            //Assert
            Assert.NotNull(response);
            Assert.IsAssignableFrom<JsonResult>(response);
            Assert.NotNull(response.SerializerSettings);
            Assert.NotNull(response.Value);
            Assert.IsAssignableFrom<string>(response.Value);
            Assert.False(string.IsNullOrWhiteSpace(response.Value.ToString()), "Response string is empty null or whitespace.");
        }
    }
}
