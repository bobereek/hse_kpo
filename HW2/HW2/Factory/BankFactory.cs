namespace HW2
{
    public class BankFactory : IBankAccountFactory, IOperationFactory, ICategoryFactory
    {
        public BankFactory() { }

        public BankAccount CreateBankAccount(int id, string name, decimal balance) => new BankAccount(id, name, balance);
        public Category CreateCategory(int id, string name, CategoryType type) => new Category(id, name, type);
        public Operation CreateOperation(int id, CategoryType type, int bankAccountId, decimal amount, DateTime date, int categoryId, string? description) => new Operation(id, type, bankAccountId, amount, date, categoryId, description);

    }
}