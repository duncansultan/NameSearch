using NameSearch.Api.Controllers;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.Models.Domain;
using System.Threading.Tasks;
using Xunit;

namespace NameSearch.Api.Tests
{
    
    public class FindPersonController_ShouldReturnJsonResult
    {
        private readonly IFindPersonController Controller;

        public FindPersonController_ShouldReturnJsonResult()
        {
            //ToDo: Consider mocking this controller.
            Controller = new FindPersonController();
        }

        [Fact]
        public async Task GetPerson()
        {
            //Arrange
            IPerson model = new Person();
            model.Address1= "";
            model.Address2= "";
            model.City= "";
            model.Country= "";
            model.Name= "";
            model.State= "";
            model.Zip= "";

            //Act
            var response = await Controller.GetPerson(model);

            //Assert
            //Http OK
            //Assert.Equal(HttpStatusCode.OK.ToString(), response.StatusCode.ToString());
            //Assert.NotNull(response.Value);
            //Assert.IsAssignableFrom<string>(response.Value);
            //Assert.False(string.IsNullOrWhiteSpace(response.Value.ToString()), "Response string is empty null or whitespace.");
            ////Deserialization Tests
            //var deserializedObject = JsonConvert.DeserializeObject(response.Value.ToString());
            //Assert.NotNull(deserializedObject);
            //var deserializedPerson = JsonConvert.DeserializeObject<IPerson>(response.Value.ToString());
            //Model Property Tests
            //Assert.NotNull(deserializedPerson);
            //Assert.NotNull(deserializedPerson.Address1);
            //Assert.NotNull(deserializedPerson.Address2);
            //Assert.NotNull(deserializedPerson.City);
            //Assert.NotNull(deserializedPerson.Country);
            //Assert.NotNull(deserializedPerson.Name);
            //Assert.NotNull(deserializedPerson.State);
            //Assert.NotNull(deserializedPerson.Zip);           
            //Assert.Same(deserializedPerson.Address1, "");
            //Assert.Same(deserializedPerson.Address2, "");
            //Assert.Same(deserializedPerson.City, "");
            //Assert.Same(deserializedPerson.Country, "");
            //Assert.Same(deserializedPerson.Name, "");
            //Assert.Same(deserializedPerson.State, "");
            //Assert.Same(deserializedPerson.Zip, "");
        }
    }
}
