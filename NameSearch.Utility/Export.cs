using CsvHelper;
using CsvHelper.Configuration;
using System.Collections;
using System.IO;

namespace NameSearch.Utility
{
    public class Export
    {
        private readonly Configuration CsvHelperConfiguration;
        private readonly string Directory;

        public Export(string directory, Configuration csvHelperConfiguration)
        {
            this.Directory = directory;
            this.CsvHelperConfiguration = csvHelperConfiguration;
        }

        public Export(string directory)
        {
            this.Directory = directory;
            this.CsvHelperConfiguration = new Configuration() { QuoteAllFields = true };
        }

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
    }
}
