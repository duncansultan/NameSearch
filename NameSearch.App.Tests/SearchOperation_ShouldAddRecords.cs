using NameSearch.Api.Controllers.Interfaces;
using NameSearch.Context;
using NameSearch.Models.Domain;
using NameSearch.Repository;

namespace NameSearch.Api.Tests
{
    public class SearchOperation_ShouldAddRecords
    {
        private readonly IFindPersonController FindPersonController;

        private readonly IEntityFrameworkRepository Repository;
        //ToDo: Mock IFindPersonController
        //private readonly IFindPersonController FindPersonController;

        public SearchOperation_ShouldAddRecords()
        {
            this.Repository = new EntityFrameworkRepository(new ApplicationDbContext());
            // FindPersonController = new FindPersonController();
        }


        public void LoadPeople()
        {
            //Arrange
            var person = new PersonSearch();
            //Act
            //var result = FindPersonController.GetPerson(person);

            //Assert
        }
    }
}
