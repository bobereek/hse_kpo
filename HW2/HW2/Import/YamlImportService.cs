using YamlDotNet.Serialization;
namespace HW2
{
    public class YamlOperation
    {
        [YamlMember(Alias = "date")]
        public DateTime Date { get; set; }

        [YamlMember(Alias = "type")]
        public string? Type { get; set; }

        [YamlMember(Alias = "amount")]
        public decimal Amount { get; set; }

        [YamlMember(Alias = "categoryId")]
        public int CategoryId { get; set; }

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }
    }

    public class YamlImportService : FileImportTemplate
    {
        public YamlImportService(
            IBankAccountStorage accountStorage,
            ICategoryStorage categoryStorage,
            IOperationStorage operationStorage)
            : base(accountStorage, categoryStorage, operationStorage)
        {
        }

        protected override List<ParsedOperation> ParseData(string rawData)
        {
            try
            {
                var deserializer = new DeserializerBuilder().Build();

                var yamlOperations = deserializer.Deserialize<List<YamlOperation>>(rawData);

                var parsedOperations = yamlOperations.Select(op => new ParsedOperation
                {
                    Date = op.Date,
                    Type = op.Type == "Income" ? CategoryType.Income : CategoryType.Expense,
                    Amount = op.Amount,
                    CategoryName = $"Category_{op.CategoryId}",
                    Description = op.Description!
                }).ToList();

                return parsedOperations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка парсинга YAML: {ex.Message}");
                return new List<ParsedOperation>();
            }
        }
    }
}