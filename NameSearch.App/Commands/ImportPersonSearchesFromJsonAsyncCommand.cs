using AutoMapper;
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
    /// Import Person Searches Command
    /// </summary>
    /// <seealso cref="NameSearch.App.Commands.Interfaces.ICommand" />
    public class ImportPersonSearchesFromJsonAsyncCommand : ICommand
    {
        /// <summary>
        /// The name
        /// </summary>
        private readonly string _fullPath;

        /// <summary>
        /// The path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// The file name
        /// </summary>
        private readonly string _fileName;

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
        /// Initializes a new instance of the <see cref="ImportPersonSearchesFromJsonAsyncCommand" /> class.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="options">The options.</param>
        public ImportPersonSearchesFromJsonAsyncCommand(string fullPath, string path, string fileName, CommandLineOptions options)
        {
            _fullPath = fullPath;
            _path = path;
            _fileName = fileName;
            _options = options;

            this.Repository = StaticServiceCollection.Repository;
            this.Import = new Import(_path);
            this.Export = new Export(_path);
            this.Mapper = MapperFactory.Get();
            this.SerializerSettings = JsonSerializerSettingsFactory.Get();

            this.ImportExport = new ImportExport(Repository, SerializerSettings, Mapper, Import, Export);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            Console.WriteLine("Hello "
                + (_fullPath != null ? _fullPath : "World")
                + (_options.IsEnthousiastic ? "!!!" : "."));

            return 0;
        }
    }
}