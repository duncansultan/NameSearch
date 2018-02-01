using NameSearch.App.Commands.Interfaces;
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