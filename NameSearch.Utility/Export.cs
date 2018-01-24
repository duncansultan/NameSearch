using CsvHelper;
using CsvHelper.Configuration;
using NameSearch.Utility.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Collections;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NameSearch.Utility
{
    /// <summary>
    /// Export Utility
    /// </summary>
    public class Export : IExport
    {
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger = Log.Logger.ForContext<Export>();

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

            var fullPath = Path.Combine(Directory, fileName);
            if (File.Exists(fullPath))
            {
                fullPath = GetAvailableFullPath(fullPath);
            }

            using (var textWriter = new StreamWriter(fullPath, isAppend))
            using (var csv = new CsvWriter(textWriter, CsvHelperConfiguration))
            {
                csv.WriteRecords(records);
            }
        }

        /// <summary>
        /// To the Txt.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref=""></exception>
        public void ToTxt(string text, string fileName)
        {
            if (!fileName.EndsWith(".txt"))
            {
                fileName = $"{fileName}.txt";
            }

            var fullPath = Path.Combine(Directory, fileName);
            if (File.Exists(fullPath))
            {
                fullPath = GetAvailableFullPath(fullPath);
            }

            using (var textWriter = new StreamWriter(fullPath))
            {
                textWriter.Write(text);
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

            var fullPath = Path.Combine(Directory, fileName);
            if (File.Exists(fullPath))
            {
                fullPath = GetAvailableFullPath(fullPath);
            }

            using (var file = File.CreateText(fullPath))
            using (var writer = new JsonTextWriter(file))
            {
                json.WriteTo(writer);
            }
        }

        /// <summary>
        /// To the txt asynchronous.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public async Task ToTxtAsync(string test, string fileName)
        {
            if (!fileName.EndsWith(".txt"))
            {
                fileName = $"{fileName}.txt";
            }

            var fullPath = Path.Combine(Directory, fileName);
            if (File.Exists(fullPath))
            {
                fullPath = GetAvailableFullPath(fullPath);
            }

            using (var textWriter = new StreamWriter(fullPath))
            {
                await textWriter.WriteAsync(test);
            }
        }

        /// <summary>
        /// To the json asynchronous.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task ToJsonAsync(JObject json, string fileName, CancellationToken cancellationToken)
        {
            if (!fileName.EndsWith(".csv"))
            {
                fileName = $"{fileName}.csv";
            }

            var fullPath = Path.Combine(Directory, fileName);
            if (File.Exists(fullPath))
            {
                fullPath = GetAvailableFullPath(fullPath);
            }

            using (var file = File.CreateText(fullPath))
            using (var writer = new JsonTextWriter(file))
            {
                await json.WriteToAsync(writer, cancellationToken);
            }
        }

        /// <summary>
        /// Gets the available full path.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns></returns>
        private string GetAvailableFullPath(string fullPath)
        {
            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
            string extension = Path.GetExtension(fullPath);
            string path = Path.GetDirectoryName(fullPath);
            string newFullPath = fullPath;

            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }

            logger.ForContext("fullPath", fullPath)
                .ForContext("newFullPath", newFullPath)
                .Information("<{EventID:l}> - {Message}", "GetAvailableFullPath", "File Exists, path updated.");

            return newFullPath;
        }
    }
}
