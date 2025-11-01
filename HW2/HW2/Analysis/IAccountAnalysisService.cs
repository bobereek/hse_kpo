namespace HW2
{
    public interface IAccountAnalysisService
    {
        AccountAnalysisResult AnalyzeAccount(int accountId);
        AccountAnalysisResult AnalyzeAccount(int accountId, DateTime fromDate, DateTime toDate);
        decimal GetTotalIncome(int accountId, DateTime? fromDate = null, DateTime? toDate = null);
        decimal GetTotalExpenses(int accountId, DateTime? fromDate = null, DateTime? toDate = null);
        decimal GetBalanceChange(int accountId, DateTime? fromDate = null, DateTime? toDate = null);
        List<CategorySummary> GetCategorySummaries(int accountId, DateTime? fromDate = null, DateTime? toDate = null);
        List<Operation> GetRecentOperations(int accountId, int count = 10);
        List<Operation> GetOperationsByCategory(int accountId, int categoryId, DateTime? fromDate = null, DateTime? toDate = null);
        List<Operation> GetOperationsByType(int accountId, CategoryType type, DateTime? fromDate = null, DateTime? toDate = null);
    }
}
