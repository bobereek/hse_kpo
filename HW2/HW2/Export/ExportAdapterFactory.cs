namespace HW2
{
    public class ExportAdapterFactory
    {
        public IExportAdapter CreateAdapter(string format)
        {
            return format.ToLower() switch
            {
                ".csv" => new CsvExportAdapter(),
                ".json" => new JsonExportAdapter(),
                ".yaml" => new YamlExportAdapter(),
                _ => throw new ArgumentException($"Unsupported export format: {format}")
            };
        }
    }
}