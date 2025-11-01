namespace HW2
{
    public class OperationStorage : IOperationStorage
    {
        private Dictionary<int, Operation> _operations = new Dictionary<int, Operation>();
        private IOperationFactory operationFactory;
        private IBankAccountStorage accountStorage;
        private ICategoryStorage categoryStorage;
        public Dictionary<int, Operation> Operations { get => _operations; }
        public OperationStorage(IOperationFactory operationFactory, IBankAccountStorage accountStorage, ICategoryStorage categoryStorage)
        {
            this.operationFactory = operationFactory;
            this.accountStorage = accountStorage;
            this.categoryStorage = categoryStorage;
        }
        public void AddOperation(int id, CategoryType type, int bankAccountId, decimal amount, DateTime date, int categoryId, string? description)
        {
            if (id <= 0)
                throw new ArgumentException("Operation ID must be positive", nameof(id));
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            if (_operations.ContainsKey(id))
                throw new InvalidOperationException($"Operation with id {id} already exists");

            var account = accountStorage.GetBankAccount(bankAccountId);
            if (account == null)
                throw new ArgumentException($"Bank account with id {bankAccountId} not found", nameof(bankAccountId));

            var category = categoryStorage.GetCategory(categoryId);
            if (category == null)
                throw new ArgumentException($"Category with id {categoryId} not found", nameof(categoryId));

            if (category.Type != type)
                throw new InvalidOperationException($"Category type {category.Type} does not match operation type {type}");

            if (type == CategoryType.Expense && account.Balance < amount)
                throw new InvalidOperationException("Insufficient funds for this operation");

            var operation = operationFactory.CreateOperation(id, type, bankAccountId, amount, date, categoryId, description);
            account.ApplyOperation(operation);
            _operations.Add(id, operation);
        }
        public void DeleteOperation(int id)
        {
            if (!_operations.TryGetValue(id, out var operation))
                throw new ArgumentException($"Operation with id {id} not found", nameof(id));

            var account = accountStorage.GetBankAccount(operation.BankAccountId);
            if (account != null)
            {
                account.RevertOperation(operation);
            }

            _operations.Remove(id);
        }

        public Operation? GetOperation(int id)
        {
            return Operations.TryGetValue(id, out var operation) ? operation : null;
        }

        public List<Operation> GetOperationsByAccount(int bankAccountId)
        {
            return Operations.Values.Where(op => op.BankAccountId == bankAccountId).ToList();
        }

        public List<Operation> GetOperationsByAccount(int bankAccountId, DateTime fromDate, DateTime toDate)
        {
            return Operations.Values.Where(op =>
                op.BankAccountId == bankAccountId &&
                op.Date >= fromDate &&
                op.Date <= toDate).ToList();
        }

        public List<Operation> GetOperationsByAccount(int bankAccountId, CategoryType type)
        {
            return Operations.Values.Where(op =>
                op.BankAccountId == bankAccountId &&
                op.Type == type).ToList();
        }

        public List<Operation> GetOperationsByAccount(int bankAccountId, int categoryId)
        {
            return Operations.Values.Where(op =>
                op.BankAccountId == bankAccountId &&
                op.CategoryId == categoryId).ToList();
        }
    }
}