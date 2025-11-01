namespace HW2
{
    public abstract class FileImportTemplate
    {
        protected readonly IBankAccountStorage _accountStorage;
        protected readonly ICategoryStorage _categoryStorage;
        protected readonly IOperationStorage _operationStorage;

        protected FileImportTemplate(
            IBankAccountStorage accountStorage,
            ICategoryStorage categoryStorage,
            IOperationStorage operationStorage)
        {
            _accountStorage = accountStorage;
            _categoryStorage = categoryStorage;
            _operationStorage = operationStorage;
        }

        public void ImportFromFile(string filePath)
        {
            try
            {
                Console.WriteLine($"Начинаем импорт из файла: {filePath}");

                var rawData = ReadFile(filePath);

                var operations = ParseData(rawData);

                ValidateData(operations);

                SaveOperations(operations);

                Console.WriteLine($"Импорт завершен успешно. Обработано {operations.Count} операций");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка импорта: {ex.Message}");
                throw;
            }
        }

        protected virtual string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
        protected abstract List<ParsedOperation> ParseData(string rawData);

        protected virtual void ValidateData(List<ParsedOperation> operations)
        {
            foreach (var operation in operations)
            {
                if (operation.Amount <= 0)
                    throw new ArgumentException($"Неверная сумма операции: {operation.Amount}");

                if (string.IsNullOrEmpty(operation.Description))
                    throw new ArgumentException("Описание операции не может быть пустым");
            }
        }

        protected virtual void SaveOperations(List<ParsedOperation> operations)
        {
            foreach (var operation in operations)
            {
                try
                {
                    var categoryId = _categoryStorage.GetOrCreateCategory(operation.CategoryName, operation.Type);

                    var operationId = IdGenerator.Generate();
                    _operationStorage.AddOperation(
                        operationId,
                        operation.Type,
                        operation.AccountId,
                        operation.Amount,
                        operation.Date,
                        categoryId,
                        operation.Description
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка сохранения операции: {ex.Message}");
                }
            }
        }

    }

    public class ParsedOperation
    {
        public DateTime Date { get; set; }
        public CategoryType Type { get; set; }
        public decimal Amount { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AccountId { get; set; } = 1;
    }
}
