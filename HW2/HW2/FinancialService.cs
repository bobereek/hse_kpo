namespace HW2
{
    public class FinancialService
    {
        private readonly IBankAccountStorage _accountStorage;
        private readonly IOperationStorage _operationStorage;
        private readonly ICategoryStorage _categoryStorage;

        public FinancialService(
            IBankAccountStorage accountStorage,
            IOperationStorage operationStorage,
            ICategoryStorage categoryStorage)
        {
            _accountStorage = accountStorage;
            _operationStorage = operationStorage;
            _categoryStorage = categoryStorage;
        }

        public void TransferMoney(int fromAccountId, int toAccountId, decimal amount, string description)
        {
            if (amount <= 0)
                throw new ArgumentException("Transfer amount must be positive", nameof(amount));

            var fromAccount = _accountStorage.GetBankAccount(fromAccountId);
            if (fromAccount == null)
                throw new ArgumentException($"Source account {fromAccountId} not found", nameof(fromAccountId));

            var toAccount = _accountStorage.GetBankAccount(toAccountId);
            if (toAccount == null)
                throw new ArgumentException($"Destination account {toAccountId} not found", nameof(toAccountId));

            if (fromAccount.Balance < amount)
                throw new InvalidOperationException("Insufficient funds for transfer");

            var transferOutCategoryId = _categoryStorage.GetOrCreateCategory("Переводы", CategoryType.Expense);
            var transferInCategoryId = _categoryStorage.GetOrCreateCategory("Переводы", CategoryType.Income);

            var transferOutId = IdGenerator.Generate();
            var transferInId = IdGenerator.Generate();

            _operationStorage.AddOperation(transferOutId, CategoryType.Expense, fromAccountId, amount, DateTime.Now, transferOutCategoryId, $"Перевод на счет {toAccountId}: {description}");
            _operationStorage.AddOperation(transferInId, CategoryType.Income, toAccountId, amount, DateTime.Now, transferInCategoryId, $"Перевод с счета {fromAccountId}: {description}");
        }

        public void CreateRecurringOperation(int accountId, int categoryId, decimal amount, string description, int intervalDays, int count)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));
            if (intervalDays <= 0)
                throw new ArgumentException("Interval must be positive", nameof(intervalDays));
            if (count <= 0)
                throw new ArgumentException("Count must be positive", nameof(count));

            var account = _accountStorage.GetBankAccount(accountId);
            if (account == null)
                throw new ArgumentException($"Account {accountId} not found", nameof(accountId));

            var category = _categoryStorage.GetCategory(categoryId);
            if (category == null)
                throw new ArgumentException($"Category {categoryId} not found", nameof(categoryId));

            var baseDate = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                var operationId = IdGenerator.Generate();
                var operationDate = baseDate.AddDays(i * intervalDays);

                var operationType = category.Type;
                _operationStorage.AddOperation(operationId, operationType, accountId, amount, operationDate, categoryId, $"{description} ({i + 1}/{count})");
            }
        }

        public void RecalculateBalance(int accountId)
        {
            var account = _accountStorage.GetBankAccount(accountId);
            if (account == null)
                throw new ArgumentException($"Account {accountId} not found");

            var operations = _operationStorage.GetOperationsByAccount(accountId);
            decimal calculatedBalance = 0;

            foreach (var operation in operations.OrderBy(op => op.Date))
            {
                if (operation.Type == CategoryType.Income)
                    calculatedBalance += operation.Amount;
                else
                    calculatedBalance -= operation.Amount;
            }

            var newAccount = new BankAccount(account.Id, account.Name, calculatedBalance);
            _accountStorage.DeleteBankAccount(accountId);
            _accountStorage.AddBankAccount(newAccount.Id, newAccount.Name, newAccount.Balance);

            Console.WriteLine($"Баланс счета {accountId} пересчитан: {calculatedBalance:C}");
        }

        public void RecalculateAllBalances()
        {
            var existingAccounts = _accountStorage.GetAllBankAccounts();
            foreach (var acc in existingAccounts)
            {
                RecalculateBalance(acc.Id);
            }
        }

    }
}
