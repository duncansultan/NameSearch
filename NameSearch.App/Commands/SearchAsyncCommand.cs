using NameSearch.App.Commands.Interfaces;
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
        /// The options
        /// </summary>
        private readonly CommandLineOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAsyncCommand" /> class.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="options">The options.</param>
        public SearchAsyncCommand(string city, string state, string zip, CommandLineOptions options)
        {
            _city = city;
            _state = state;
            _zip = zip;
            _options = options;
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