namespace HW2
{
    public interface IExportAdapter
    {
        void Export(List<Operation> operations, string filePath);
        string GetFileExtension();
    }
}