namespace HW2
{
    public class JsonImportService : FileImportTemplate
    {
        public JsonImportService(
            IBankAccountStorage accountStorage,
            ICategoryStorage categoryStorage,
            IOperationStorage operationStorage)
            : base(accountStorage, categoryStorage, operationStorage)
        {
        }

        protected override List<ParsedOperation> ParseData(string rawData)
        {
            var operations = new List<ParsedOperation>();

            try
            {
                var jsonOperations = System.Text.Json.JsonSerializer.Deserialize<List<JsonOperationData>>(rawData);

                if (jsonOperations == null) return operations;

                foreach (var jsonOp in jsonOperations)
                {
                    try
                    {
                        var operation = new ParsedOperation
                        {
                            Date = DateTime.Parse(jsonOp.Date),
                            Type = jsonOp.Type == "Income" ? CategoryType.Income : CategoryType.Expense,
                            Amount = jsonOp.Amount,
                            CategoryName = $"Category_{jsonOp.CategoryId}",
                            Description = jsonOp.Description
                        };

                        operations.Add(operation);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" Ошибка парсинга JSON операции: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Ошибка парсинга JSON файла: {ex.Message}");
            }

            return operations;
        }
    }

    public class JsonOperationData
    {
        public string Date { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
