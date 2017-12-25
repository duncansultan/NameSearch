using CsvHelper.Configuration;

namespace NameSearch.Utility
{
    public class Export
    {
        private readonly Configuration CsvHelperConfiguration;
        private readonly string OutputDirectory;

        public Export(Configuration csvHelperConfiguration, string outputDirectory)
        {
            this.CsvHelperConfiguration = csvHelperConfiguration;
            this.OutputDirectory = outputDirectory;
        }

        public void ToCsv<T>(T model, string fileName)
        {
            //ToDo: Use CSV Helper to Export
        }
    }
}
