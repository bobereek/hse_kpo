namespace HW2
{
    public interface IOperationStorage
    {
        Dictionary<int, Operation> Operations { get; }
        void AddOperation(int id, CategoryType type, int bankAccountId, decimal amount, DateTime date, int categoryId, string? description);
        void DeleteOperation(int id);

        Operation? GetOperation(int id);
        List<Operation> GetOperationsByAccount(int bankAccountId);
        List<Operation> GetOperationsByAccount(int bankAccountId, DateTime fromDate, DateTime toDate);
        List<Operation> GetOperationsByAccount(int bankAccountId, CategoryType type);
        List<Operation> GetOperationsByAccount(int bankAccountId, int categoryId);
    }
}