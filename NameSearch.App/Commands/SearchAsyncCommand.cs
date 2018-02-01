using AutoMapper;
using NameSearch.Api.Controllers.Interfaces;
using NameSearch.App.Commands.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using System;

namespace NameSearch.App.Commands
{
    /// <summary>
    /// Search Command
    /// </summary>
    /// <seealso cref="NameSearch.App.Commands.Interfaces.ICommand" />
    public class SearchAsyncCommand : ICommand
    {
        /// <summary>
        /// The city
        /// </summary>
        private readonly string _city;

        /// <summary>
        /// The state
        /// </summary>
        private readonly string _state;

        /// <summary>
        /// The zip
        /// </summary>
        private readonly string _zip;

        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// The options
        /// </summary>
        private readonly CommandLineOptions _options;

        #region Dependencies

        /// <summary>
        /// The export
        /// </summary>
        private readonly IExport Export;

        /// <summary>
        /// The mapper
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        public IMapper Mapper { get; set; }

        /// <summary>
        /// The repository
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public IEntityFrameworkRepository Repository { get; set; }

        /// <summary>
        /// The serializer settings
        /// </summary>
        /// <value>
        /// The serializer settings.
        /// </value>
        public JsonSerializerSettings SerializerSettings { get; set; }

        /// <summary>
        /// Gets or sets the find person controller.
        /// </summary>
        /// <value>
        /// The find person controller.
        /// </value>
        public IFindPersonController FindPersonController { get; set; }

        /// <summary>
        /// The people search
        /// </summary>
        private readonly PeopleSearch PeopleSearch;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAsyncCommand" /> class.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="options">The options.</param>
        public SearchAsyncCommand(string city, string state, string zip, string path, CommandLineOptions options)
        {
            _city = city;
            _state = state;
            _zip = zip;
            _path = path;
            _options = options;

            this.Repository = StaticServiceCollection.Repository;
            this.Export = new Export(_path);
            this.Mapper = MapperFactory.Get();
            this.SerializerSettings = JsonSerializerSettingsFactory.Get();

            this.PeopleSearch = new PeopleSearch(Repository, FindPersonController, SerializerSettings, Mapper, Export);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            Console.WriteLine("Hello "
                + (_city != null ? _city : "World")
                + (_options.IsEnthousiastic ? "!!!" : "."));

            return 0;
        }
    }
}