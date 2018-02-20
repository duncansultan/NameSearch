using NameSearch.App.Commands.Interfaces;
using NameSearch.App.Services;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportPeopleCommand" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ExportPeopleCommand(CommandLineOptions options)
        {
            _options = options;

            _fullPath = Path.Combine(Program.ExportDirectory, $"PeopleExport-{DateTime.Now}.csv");
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run(IPeopleSearchService peopleSearchService)
        {
            peopleSearchService.ExportToCsv(_fullPath);

            return 0;
        }
    }
}