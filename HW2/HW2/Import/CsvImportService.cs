namespace HW2
{
    public class CsvImportService : FileImportTemplate
    {
        public CsvImportService(
            IBankAccountStorage accountStorage,
            ICategoryStorage categoryStorage,
            IOperationStorage operationStorage)
            : base(accountStorage, categoryStorage, operationStorage)
        {
        }


        protected override List<ParsedOperation> ParseData(string rawData)
        {
            var operations = new List<ParsedOperation>();
            var lines = rawData.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length < 5) continue;

                try
                {
                    var operation = new ParsedOperation
                    {
                        Date = DateTime.Parse(parts[0]),
                        Type = parts[1] == "Income" ? CategoryType.Income : CategoryType.Expense,
                        Amount = decimal.Parse(parts[2]),
                        CategoryName = parts[3],
                        Description = parts[4].Trim('"')
                    };

                    operations.Add(operation);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка парсинга строки {i}: {ex.Message}");
                }
            }

            return operations;
        }
    }
}
