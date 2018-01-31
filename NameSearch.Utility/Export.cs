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
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<Export>();

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
        public void ToCsv<T>(IEnumerable<T> records, string fileName, bool isAppend)
        {
            var log = logger.With("IEnumerable<T>", typeof(T))
                .With("records", records.Count())
                .With("fileName", fileName)
                .With("Directory", Directory)
                .With("isAppend", isAppend);

            if (!fileName.EndsWith(".csv"))
            {
                fileName = $"{fileName}.csv";

                log.InformationEvent("ToCsv", "Invalid file extension, adding .csv extension");
            }

            var fullPath = Path.Combine(Directory, fileName);

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (fileExists)
            {
                fullPath = GetAvailableFullPath(fullPath);

                log.InformationEvent("ToCsv", "File already exists at path, getting new filename of {filename}", fullPath);
            }

            using (var textWriter = new StreamWriter(fullPath, isAppend))
            using (var csv = new CsvWriter(textWriter, CsvHelperConfiguration))
            {
                csv.WriteRecords(records);
            }

            log.InformationEvent("ToCsv", "Saved {records} records successfully", records.Count());
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="fileName">Name of the file.</param>
        public void ToJson(JObject json, string fileName)
        {
            var log = logger.With("tokens", json.Count)
                            .With("fileName", fileName)
                            .With("Directory", Directory);

            if (!fileName.EndsWith(".json"))
            {
                fileName = $"{fileName}.json";

                log.InformationEvent("ToJson", "Invalid file extension, adding .json extension");
            }

            var fullPath = Path.Combine(Directory, fileName);

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (fileExists)
            {
                fullPath = GetAvailableFullPath(fullPath);

                log.InformationEvent("ToJson", "File already exists at path, getting new filename of {filename}", fullPath);
            }

            using (var file = File.CreateText(fullPath))
            using (var writer = new JsonTextWriter(file))
            {
                json.WriteTo(writer);
            }

            log.InformationEvent("ToJson", "Saved JSON with {tokens} tokens successfully", json.Count);
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
            var log = logger.With("tokens", json.Count)
                            .With("fileName", fileName)
                            .With("Directory", Directory);

            if (!fileName.EndsWith(".json"))
            {
                fileName = $"{fileName}.json";

                log.InformationEvent("ToJson", "Invalid file extension, adding .json extension");
            }

            var fullPath = Path.Combine(Directory, fileName);

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (fileExists)
            {
                fullPath = GetAvailableFullPath(fullPath);

                log.InformationEvent("ToJson", "File already exists at path, getting new filename of {filename}", fullPath);
            }

            using (var file = File.CreateText(fullPath))
            using (var writer = new JsonTextWriter(file))
            {
                await json.WriteToAsync(writer, cancellationToken);
            }

            log.InformationEvent("ToJsonAsync", "Saved JSON with {tokens} tokens successfully", json.Count);
        }

        /// <summary>
        /// To the Txt.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref=""></exception>
        public void ToTxt(string text, string fileName)
        {
            var log = logger.With("length", text.Length)
                .With("fileName", fileName)
                .With("Directory", Directory);

            if (!fileName.EndsWith(".txt"))
            {
                fileName = $"{fileName}.txt";

                log.InformationEvent("ToTxt", "Invalid file extension, adding .txt extension");
            }

            var fullPath = Path.Combine(Directory, fileName);

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (fileExists)
            {
                fullPath = GetAvailableFullPath(fullPath);

                log.InformationEvent("ToTxt", "File already exists at path, getting new filename of {filename}", fullPath);
            }

            using (var textWriter = new StreamWriter(fullPath))
            {
                textWriter.Write(text);
            }

            log.InformationEvent("ToTxt", "Saved {records} text characters successfully", text.Length);
        }

        /// <summary>
        /// To the txt asynchronous.
        /// </summary>
        /// <param name="text">The test.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public async Task ToTxtAsync(string text, string fileName)
        {
            var log = logger.With("length", text.Length)
                .With("fileName", fileName)
                .With("Directory", Directory);

            if (!fileName.EndsWith(".txt"))
            {
                fileName = $"{fileName}.txt";

                log.InformationEvent("ToTxtAsync", "Invalid file extension, adding .txt extension");
            }

            var fullPath = Path.Combine(Directory, fileName);

            log.With("fullPath", fullPath);

            var fileExists = File.Exists(fullPath);

            log.With("fileExists", fileExists);

            if (fileExists)
            {
                fullPath = GetAvailableFullPath(fullPath);

                log.InformationEvent("ToTxtAsync", "File already exists at path, getting new filename of {filename}", fullPath);
            }

            using (var textWriter = new StreamWriter(fullPath))
            {
                await textWriter.WriteAsync(text);
            }

            log.InformationEvent("ToTxtAsync", "Saved {records} text characters successfully", text.Length);
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

            return newFullPath;
        }
    }
}