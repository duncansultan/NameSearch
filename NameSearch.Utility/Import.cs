using CsvHelper;
using CsvHelper.Configuration;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.Utility
{
    /// <summary>
    /// Import Utility
    /// </summary>
    public class Import : IImport
    {
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger = Log.Logger.ForContext<Import>();

        /// <summary>
        /// The CSV helper configuration
        /// </summary>
        private readonly Configuration CsvHelperConfiguration;
        /// <summary>
        /// The directory
        /// </summary>
        private readonly string Directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Import" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="csvHelperConfiguration">The CSV helper configuration.</param>
        public Import(string directory, Configuration csvHelperConfiguration)
        {
            this.Directory = directory;
            this.CsvHelperConfiguration = csvHelperConfiguration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Import" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public Import(string directory)
        {
            this.Directory = directory;
            this.CsvHelperConfiguration = new Configuration() { QuoteAllFields = true };
        }

        /// <summary>
        /// Froms the CSV.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public IEnumerable<T> FromCsv<T>(string fileName)
        {
            var fullPath = Path.Combine(Directory, fileName);

            var fileExists = File.Exists(fullPath);
            if (!fileExists)
            {
                logger.ForContext("fullPath", fullPath)
                    .ForContext("fileExists", fileExists)
                    .Information("<{EventID:l}> - {Message}", "GetAvailableFullPath", "File not found.");

                throw new FileNotFoundException("FromCsv - File not found.", fullPath);
            }

            using (var textReader = new StreamReader(fullPath))
            using (var csv = new CsvReader(textReader, CsvHelperConfiguration))
            {
                var records = csv.GetRecords<T>();
                return records;
            }
        }

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public JObject FromJson(string fileName)
        {
            var fullPath = Path.Combine(Directory, fileName);

            var fileExists = File.Exists(fullPath);
            if (!fileExists)
            {
                logger.ForContext("fullPath", fullPath)
                    .ForContext("fileExists", fileExists)
                    .Information("<{EventID:l}> - {Message}", "FromJson", "File not found.");

                throw new FileNotFoundException("FromJson - File not found.", fullPath);
            }

            using (var file = File.OpenText(fullPath))
            using (var reader = new JsonTextReader(file))
            {
                JObject jObject = (JObject)JToken.ReadFrom(reader);
                return jObject;
            }
        }

        /// <summary>
        /// Froms the json asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public async Task<JObject> FromJsonAsync(string fileName, CancellationToken cancellationToken)
        {
            var fullPath = Path.Combine(Directory, fileName);

            var fileExists = File.Exists(fullPath);
            if (!fileExists)
            {
                logger.ForContext("fullPath", fullPath)
                    .ForContext("fileExists", fileExists)
                    .Information("<{EventID:l}> - {Message}", "FromJsonAsync", "File not found.");

                throw new FileNotFoundException("FromJsonAsync - File not found.", fullPath);
            }

            using (var file = File.OpenText(fullPath))
            using (var reader = new JsonTextReader(file))
            {
                JObject jObject = (JObject)await JToken.ReadFromAsync(reader, cancellationToken);
                return jObject;
            }
        }
    }
}
