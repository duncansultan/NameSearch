using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;

namespace NameSearch.Utility
{
    /// <summary>
    /// Import Utility
    /// </summary>
    public class Import
    {
        /// <summary>
        /// The CSV helper configuration
        /// </summary>
        private readonly Configuration CsvHelperConfiguration;
        /// <summary>
        /// The directory
        /// </summary>
        private readonly string Directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Import"/> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="csvHelperConfiguration">The CSV helper configuration.</param>
        public Import(string directory, Configuration csvHelperConfiguration)
        {
            this.Directory = directory;
            this.CsvHelperConfiguration = csvHelperConfiguration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Import"/> class.
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
        /// <param name="isAppend">if set to <c>true</c> [is append].</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public IEnumerable<T> FromCsv<T>(string fileName, bool isAppend)
        {
            var absolutePath = Path.Combine(Directory, fileName);

            var fileExists = File.Exists(absolutePath);
            if (!fileExists)
            {
                throw new FileNotFoundException($"File {fileName} does not exist.");
            }

            using (var textReader = new StreamReader(absolutePath, isAppend))
            using (var csv = new CsvReader(textReader, CsvHelperConfiguration))
            {
                var records = csv.GetRecords<T>();
                return records;
            }
        }
    }
}
