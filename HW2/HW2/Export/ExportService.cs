namespace HW2
{
    public class ExportService
    {
        private readonly ExportAdapterFactory _factory;

        public ExportService()
        {
            _factory = new ExportAdapterFactory();
        }

        public void ExportOperations(List<Operation> operations, string filePath)
        {
            string format = Path.GetExtension(filePath);
            var adapter = _factory.CreateAdapter(format);
            adapter.Export(operations, filePath);
        }
    }
}