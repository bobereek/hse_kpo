namespace HW2
{


    public class CsvExportAdapter : IExportAdapter
    {
        public void Export(List<Operation> operations, string filePath)
        {
            var csvContent = new List<string> { "Date,Type,Amount,Category,Description" };

            foreach (var operation in operations.OrderByDescending(op => op.Date))
            {
                var type = operation.Type == CategoryType.Income ? "Income" : "Expense";
                csvContent.Add($"{operation.Date:yyyy-MM-dd},{type},{operation.Amount},Category_{operation.CategoryId},\"{operation.Description}\"");
            }

            File.WriteAllLines(filePath, csvContent);
        }

        public string GetFileExtension() => ".csv";
    }

    public class JsonExportAdapter : IExportAdapter
    {
        public void Export(List<Operation> operations, string filePath)
        {
            var jsonOperations = operations.Select(op => new
            {
                Date = op.Date.ToString("yyyy-MM-dd"),
                Type = op.Type.ToString(),
                Amount = op.Amount,
                CategoryId = op.CategoryId,
                Description = op.Description
            }).ToList();

            var json = System.Text.Json.JsonSerializer.Serialize(jsonOperations, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }

        public string GetFileExtension() => ".json";
    }

    public class YamlExportAdapter : IExportAdapter
    {
        public void Export(List<Operation> operations, string filePath)
        {
            var yaml = new List<string> { "operations:" };

            foreach (var operation in operations.OrderByDescending(op => op.Date))
            {
                yaml.Add($"  - date: {operation.Date:yyyy-MM-dd}");
                yaml.Add($"    type: {operation.Type}");
                yaml.Add($"    amount: {operation.Amount}");
                yaml.Add($"    categoryId: {operation.CategoryId}");
                yaml.Add($"    description: \"{operation.Description}\"");
                yaml.Add("");
            }

            File.WriteAllLines(filePath, yaml);
        }

        public string GetFileExtension() => ".yaml";
    }



}
