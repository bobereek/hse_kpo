using System.ComponentModel;

namespace HW2
{
    public class BankFacade
    {
        private readonly IBankAccountStorage _accountStorage;
        private readonly ICategoryStorage _categoryStorage;
        private readonly IOperationStorage _operationStorage;
        private readonly IAccountAnalysisService _analysisService;
        private readonly FinancialService _financialService;
        private readonly ExportService _exportService;
        private readonly Report _report;

        public BankFacade(
            IBankAccountStorage accountStorage,
            ICategoryStorage categoryStorage,
            IOperationStorage operationStorage,
            IAccountAnalysisService analysisService,
            FinancialService financialService,
            ExportService exportService,
            Report report)
        {
            _accountStorage = accountStorage;
            _categoryStorage = categoryStorage;
            _operationStorage = operationStorage;
            _analysisService = analysisService;
            _financialService = financialService;
            _exportService = exportService;
            _report = report;
        }

        public void CreateAccount(string name, decimal initialBalance = 0)
        {
            var accountId = IdGenerator.Generate();
            _accountStorage.AddBankAccount(accountId, name, initialBalance);
        }

        public void CreateCategory(string name, CategoryType type)
        {
            var categoryId = IdGenerator.Generate();
            _categoryStorage.AddCategory(categoryId, name, type);
        }

        public void AddTransaction(int accountId, int categoryId, decimal amount, string description)
        {
            var category = _categoryStorage.GetCategory(categoryId);
            if (category == null)
                throw new ArgumentException($"Category {categoryId} not found");

            var operationId = IdGenerator.Generate();
            _operationStorage.AddOperation(operationId, category.Type, accountId, amount, DateTime.Now, categoryId, description);
        }

        public void TransferMoney(int fromAccountId, int toAccountId, decimal amount, string description)
        {
            _financialService.TransferMoney(fromAccountId, toAccountId, amount, description);
        }

        public void PrintAccountSummary(int accountId)
        {
            _report.PrintAccountAnalysis(accountId);
        }

        public void PrintAccountSummary(int accountId, DateTime fromDate, DateTime toDate)
        {
            _report.PrintAccountAnalysis(accountId, fromDate, toDate);
        }

        public void ExportAccountData(int accountId, string filePath, DateTime fromDate, DateTime toDate)
        {
            List<Operation> operations = _operationStorage.GetOperationsByAccount(accountId, fromDate, toDate);
            _exportService.ExportOperations(operations, filePath);
        }

        public Dictionary<string, decimal> GetCategoryStatistics(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var result = new Dictionary<string, decimal>();
            var accounts = _accountStorage.GetAllBankAccounts();
            foreach (var acc in accounts)
            {
                var summaries = _analysisService.GetCategorySummaries(acc.Id, fromDate, toDate);
                foreach (var s in summaries)
                {
                    if (result.ContainsKey(s.CategoryName))
                        result[s.CategoryName] += s.TotalAmount;
                    else
                        result[s.CategoryName] = s.TotalAmount;
                }
            }

            return result;
        }

        public void CreateRecurringTransaction(int accountId, int categoryId, decimal amount, string description, int intervalDays, int count)
        {
            _financialService.CreateRecurringOperation(accountId, categoryId, amount, description, intervalDays, count);
        }

        // Вспомогательные методы
        public BankAccount? GetAccount(int accountId)
        {
            return _accountStorage.GetBankAccount(accountId);
        }

        public Category? GetCategory(int categoryId)
        {
            return _categoryStorage.GetCategory(categoryId);
        }

        public List<Operation> GetAccountOperations(int accountId)
        {
            return _operationStorage.GetOperationsByAccount(accountId).ToList();
        }

        public List<Operation> GetAccountOperations(int accountId, DateTime fromDate, DateTime toDate)
        {
            return _operationStorage.GetOperationsByAccount(accountId, fromDate, toDate).ToList();
        }

    }
}
