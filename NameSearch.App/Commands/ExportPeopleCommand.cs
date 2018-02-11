using AutoMapper;
using NameSearch.App.Commands.Interfaces;
using NameSearch.App.Factories;
using NameSearch.App.Services;
using NameSearch.Repository.Interfaces;
using NameSearch.Utility;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace NameSearch.App.Commands
{
    /// <summary>
    /// Export People Command
    /// </summary>
    /// <seealso cref="NameSearch.App.Commands.Interfaces.ICommand" />
    public class ExportPeopleCommand : ICommand
    {
        /// <summary>
        /// The name
        /// </summary>
        private readonly string _fullPath;

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
        /// The import
        /// </summary>
        private readonly IImport Import;

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
        /// The import export
        /// </summary>
        private readonly ImportExport ImportExport;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportPeopleCommand" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ExportPeopleCommand(CommandLineOptions options)
        {
            _options = options;

            this.Repository = Program.Repository;
            this.Import = new Import();
            this.Export = new Export();
            this.Mapper = MapperFactory.Get();
            this.SerializerSettings = JsonSerializerSettingsFactory.Get();

            _fullPath = Path.Combine(Program.ExportDirectory, $"PeopleExport-{DateTime.Now}.csv");

            this.ImportExport = new ImportExport(Repository, SerializerSettings, Mapper, Import, Export);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            this.ImportExport.ExportSearches(_fullPath);

            return 0;
        }
    }
}