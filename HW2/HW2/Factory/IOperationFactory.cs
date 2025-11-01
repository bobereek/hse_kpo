namespace HW2
{
    public interface IOperationFactory
    {
        public Operation CreateOperation(int id, CategoryType type, int bankAccountId, decimal amount, DateTime date, int categoryId, string? description);
    }
}