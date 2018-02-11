using CsvHelper;
using CsvHelper.Configuration;
using NameSearch.Extensions;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.Utility
{
    /// <summary>
    /// Import Utility
    /// </summary>
    /// <seealso cref="NameSearch.Utility.Interfaces.IImport" />
    public class Import : IImport
    {
        /// <summary>
        /// The CSV helper configuration
        /// </summary>
        private readonly Configuration CsvHelperConfiguration;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<Import>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Import" /> class.
        /// </summary>
        /// <param name="csvHelperConfiguration">The CSV helper configuration.</param>
        public Import(Configuration csvHelperConfiguration)
        {
            this.CsvHelperConfiguration = csvHelperConfiguration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Import" /> class.
        /// </summary>
        public Import()
        {
            this.CsvHelperConfiguration = new Configuration() { QuoteAllFields = true };
        }

        /// <summary>
        /// Froms the text.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">FromJson - File not found</exception>
        public IEnumerable<string> FromTxt(string fullPath)
        {
            var log = logger.With("fileName", fullPath);

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (!fileExists)
            {
                log.ErrorEvent("FromTxt", "File not found");

                throw new FileNotFoundException("FromTxt - File not found", fullPath);
            }

            var textLines = File.ReadAllLines(fullPath).ToList();

            log.InformationEvent("FromTxt", "Imported {lines} lines successfully from file {fullPath}", textLines.Count(), fullPath);

            return textLines;
        }

        /// <summary>
        /// Froms the text asynchronous.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">FromJson - File not found</exception>
        public async Task<IEnumerable<string>> FromTxtAsync(string fullPath, CancellationToken cancellationToken)
        {
            var log = logger.With("fileName", fullPath);

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (!fileExists)
            {
                log.ErrorEvent("FromTxtAsync", "File not found");

                throw new FileNotFoundException("FromTxtAsync - File not found", fullPath);
            }

            var textLines = (await File.ReadAllLinesAsync(fullPath, cancellationToken)).ToList();

            log.InformationEvent("FromTxtAsync", "Imported {lines} lines successfully from file {fullPath}", textLines.Count(), fullPath);

            return textLines;
        }

        /// <summary>
        /// Froms the CSV.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">FromCsv - File not found</exception>
        public IEnumerable<T> FromCsv<T>(string fullPath)
        {
            var log = logger.With("fullPath", fullPath)
                .With("IEnumerable<T>", typeof(T));

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (!fileExists)
            {
                log.ErrorEvent("FromCsv", "File not found");

                throw new FileNotFoundException("FromCsv - File not found", fullPath);
            }

            using (var textReader = new StreamReader(fullPath))
            using (var csv = new CsvReader(textReader, CsvHelperConfiguration))
            {
                var records = csv.GetRecords<T>();

                log.InformationEvent("FromCsv", "Imported {rows} rows successfully from file {fullPath}", records.Count(), fullPath);

                return records;
            }
        }

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">FromJson - File not found</exception>
        public JObject FromJson(string fullPath)
        {
            var log = logger.With("fileName", fullPath);

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (!fileExists)
            {
                log.ErrorEvent("FromJson", "File not found");

                throw new FileNotFoundException("FromJson - File not found", fullPath);
            }

            using (var file = File.OpenText(fullPath))
            using (var reader = new JsonTextReader(file))
            {
                JObject jObject = (JObject)JToken.ReadFrom(reader);

                log.InformationEvent("FromJson", "Imported JObject with {tokens} tokens successfully from file {fullPath}", jObject.Count, fullPath);

                return jObject;
            }
        }

        /// <summary>
        /// Froms the json asynchronous.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">FromJsonAsync - File not found</exception>
        public async Task<JObject> FromJsonAsync(string fullPath, CancellationToken cancellationToken)
        {
            var log = logger.With("fileName", fullPath);

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (!fileExists)
            {
                log.ErrorEvent("FromJsonAsync", "File not found");

                throw new FileNotFoundException("FromJsonAsync - File not found", fullPath);
            }

            using (var file = File.OpenText(fullPath))
            using (var reader = new JsonTextReader(file))
            {
                JObject jObject = (JObject)await JToken.ReadFromAsync(reader, cancellationToken);

                log.InformationEvent("FromJsonAsync", "Imported JObject with {tokens} tokens successfully from file {fullPath}", jObject.Count, fullPath);

                return jObject;
            }
        }
    }
}