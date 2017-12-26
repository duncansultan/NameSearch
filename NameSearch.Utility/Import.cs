using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;

namespace NameSearch.Utility
{
    public class Import
    {
        private readonly Configuration CsvHelperConfiguration;
        private readonly string Directory;

        public Import(string directory, Configuration csvHelperConfiguration)
        {
            this.Directory = directory;
            this.CsvHelperConfiguration = csvHelperConfiguration;
        }

        public Import(string directory)
        {
            this.Directory = directory;
            this.CsvHelperConfiguration = new Configuration() { QuoteAllFields = true };
        }

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
