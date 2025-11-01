namespace HW2
{
    public class AccountAnalysisService : IAccountAnalysisService
    {
        private readonly IBankAccountStorage _accountStorage;
        private readonly IOperationStorage _operationStorage;
        private readonly ICategoryStorage _categoryStorage;

        public AccountAnalysisService(IBankAccountStorage accountStorage, IOperationStorage operationStorage, ICategoryStorage categoryStorage)
        {
            _accountStorage = accountStorage;
            _operationStorage = operationStorage;
            _categoryStorage = categoryStorage;
        }

        public AccountAnalysisResult AnalyzeAccount(int accountId)
        {
            var account = _accountStorage.GetBankAccount(accountId);
            if (account == null)
                throw new ArgumentException($"Account with id {accountId} not found");

            var operations = _operationStorage.GetOperationsByAccount(accountId).ToList();

            return CreateAnalysisResult(account, operations, null, null);
        }

        public AccountAnalysisResult AnalyzeAccount(int accountId, DateTime fromDate, DateTime toDate)
        {
            var account = _accountStorage.GetBankAccount(accountId);
            if (account == null)
                throw new ArgumentException($"Account with id {accountId} not found");

            var operations = _operationStorage.GetOperationsByAccount(accountId, fromDate, toDate).ToList();

            return CreateAnalysisResult(account, operations, fromDate, toDate);
        }

        public decimal GetTotalIncome(int accountId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var operations = GetFilteredOperations(accountId, fromDate, toDate);
            return operations.Where(op => op.Type == CategoryType.Income).Sum(op => op.Amount);
        }

        public decimal GetTotalExpenses(int accountId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var operations = GetFilteredOperations(accountId, fromDate, toDate);
            return operations.Where(op => op.Type == CategoryType.Expense).Sum(op => op.Amount);
        }

        public decimal GetBalanceChange(int accountId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var totalIncome = GetTotalIncome(accountId, fromDate, toDate);
            var totalExpenses = GetTotalExpenses(accountId, fromDate, toDate);
            return totalIncome - totalExpenses;
        }

        public List<CategorySummary> GetCategorySummaries(int accountId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var operations = GetFilteredOperations(accountId, fromDate, toDate);
            var totalAmount = operations.Sum(op => op.Amount);

            var categoryGroups = operations.GroupBy(op => op.CategoryId);
            var summaries = new List<CategorySummary>();

            foreach (var group in categoryGroups)
            {
                var category = _categoryStorage.GetCategory(group.Key);
                if (category == null) continue;

                var categoryTotal = group.Sum(op => op.Amount);
                var percentage = totalAmount > 0 ? (categoryTotal / totalAmount) * 100 : 0;

                summaries.Add(new CategorySummary
                {
                    CategoryId = group.Key,
                    CategoryName = category.Name,
                    Type = category.Type,
                    TotalAmount = categoryTotal,
                    OperationCount = group.Count(),
                    Percentage = Math.Round(percentage, 2)
                });
            }

            return summaries.OrderByDescending(s => s.TotalAmount).ToList();
        }

        public List<Operation> GetRecentOperations(int accountId, int count = 10)
        {
            return _operationStorage.GetOperationsByAccount(accountId)
                .OrderByDescending(op => op.Date)
                .Take(count)
                .ToList();
        }

        public List<Operation> GetOperationsByCategory(int accountId, int categoryId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var operations = GetFilteredOperations(accountId, fromDate, toDate);
            return operations.Where(op => op.CategoryId == categoryId)
                .OrderByDescending(op => op.Date)
                .ToList();
        }

        public List<Operation> GetOperationsByType(int accountId, CategoryType type, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var operations = GetFilteredOperations(accountId, fromDate, toDate);
            return operations.Where(op => op.Type == type)
                .OrderByDescending(op => op.Date)
                .ToList();
        }

        private List<Operation> GetFilteredOperations(int accountId, DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate.HasValue && toDate.HasValue)
                return _operationStorage.GetOperationsByAccount(accountId, fromDate.Value, toDate.Value).ToList();
            else
                return _operationStorage.GetOperationsByAccount(accountId).ToList();
        }

        private AccountAnalysisResult CreateAnalysisResult(BankAccount account, List<Operation> operations, DateTime? fromDate, DateTime? toDate)
        {
            var totalIncome = operations.Where(op => op.Type == CategoryType.Income).Sum(op => op.Amount);
            var totalExpenses = operations.Where(op => op.Type == CategoryType.Expense).Sum(op => op.Amount);
            var categorySummaries = GetCategorySummaries(account.Id, fromDate, toDate);
            var recentOperations = GetRecentOperations(account.Id, 5);

            return new AccountAnalysisResult
            {
                AccountId = account.Id,
                AccountName = account.Name,
                CurrentBalance = account.Balance,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                TotalOperations = operations.Count,
                AnalysisDate = DateTime.Now,
                PeriodStart = fromDate,
                PeriodEnd = toDate,
                CategorySummaries = categorySummaries,
                RecentOperations = recentOperations
            };
        }
    }
}
