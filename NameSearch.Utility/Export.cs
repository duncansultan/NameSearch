using CsvHelper;
using CsvHelper.Configuration;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;

namespace NameSearch.Utility
{
    /// <summary>
    /// Export Utility
    /// </summary>
    public class Export : IExport
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
        /// Initializes a new instance of the <see cref="Export" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="csvHelperConfiguration">The CSV helper configuration.</param>
        public Export(string directory, Configuration csvHelperConfiguration)
        {
            this.Directory = directory;
            this.CsvHelperConfiguration = csvHelperConfiguration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Export" /> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public Export(string directory)
        {
            this.Directory = directory;
            this.CsvHelperConfiguration = new Configuration() { QuoteAllFields = true };
        }

        /// <summary>
        /// To the CSV.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records">The records.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="isAppend">if set to <c>true</c> [is append].</param>
        public void ToCsv<T>(T records, string fileName, bool isAppend)
            where T : IEnumerable
        {
            if (!fileName.EndsWith(".csv"))
            {
                fileName = $"{fileName}.csv";
            }
            var absolutePath = Path.Combine(Directory, fileName);


            using (var textWriter = new StreamWriter(absolutePath, isAppend))
            using (var csv = new CsvWriter(textWriter, CsvHelperConfiguration))
            {
                csv.WriteRecords(records);
            }
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="fileName">Name of the file.</param>
        public void ToJson(string json, string fileName)
        {
            if (!fileName.EndsWith(".json"))
            {
                fileName = $"{fileName}.json";
            }
            var absolutePath = Path.Combine(Directory, fileName);

            using (var textWriter = new StreamWriter(absolutePath))
            {
                textWriter.Write(json);
            }
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="fileName">Name of the file.</param>
        public void ToJson(JObject json, string fileName)
        {
            if (!fileName.EndsWith(".csv"))
            {
                fileName = $"{fileName}.csv";
            }
            var absolutePath = Path.Combine(Directory, fileName);

            using (var file = File.CreateText(absolutePath))
            using (var writer = new JsonTextWriter(file))
            {
                json.WriteTo(writer);
            }
        }
    }
}
