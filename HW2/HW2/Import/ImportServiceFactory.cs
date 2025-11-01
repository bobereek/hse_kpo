namespace HW2
{
    public class ImportServiceFactory
    {
        private readonly IBankAccountStorage _accountStorage;
        private readonly ICategoryStorage _categoryStorage;
        private readonly IOperationStorage _operationStorage;

        public ImportServiceFactory(
            IBankAccountStorage accountStorage,
            ICategoryStorage categoryStorage,
            IOperationStorage operationStorage)
        {
            _accountStorage = accountStorage;
            _categoryStorage = categoryStorage;
            _operationStorage = operationStorage;
        }

        public FileImportTemplate CreateImporter(string fileExtension)
        {
            return fileExtension.ToLower() switch
            {
                ".csv" => new CsvImportService(_accountStorage, _categoryStorage, _operationStorage),
                ".json" => new JsonImportService(_accountStorage, _categoryStorage, _operationStorage),
                ".yaml" => new YamlImportService(_accountStorage, _categoryStorage, _operationStorage),
                _ => throw new ArgumentException($"Неподдерживаемый формат файла: {fileExtension}")
            };
        }

        public FileImportTemplate CreateImporterFromPath(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            return CreateImporter(extension);
        }
    }
}
