﻿using NameSearch.App.Commands.Interfaces;
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

        #region Dependencies

        /// <summary>
        /// The import export
        /// </summary>
        private readonly PeopleSearch PeopleSearch;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportPeopleCommand" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ExportPeopleCommand(CommandLineOptions options)
        {
            _options = options;

            _fullPath = Path.Combine(Program.ExportDirectory, $"PeopleExport-{DateTime.Now}.csv");

            this.PeopleSearch = new PeopleSearch(Program.Repository, Program.Configuration);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            this.PeopleSearch.ExportToCsv(_fullPath);

            return 0;
        }
    }
}