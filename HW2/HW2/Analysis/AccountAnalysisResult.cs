namespace HW2
{
    public class AccountAnalysisResult
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public decimal CurrentBalance { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public int TotalOperations { get; set; }
        public DateTime AnalysisDate { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public List<CategorySummary> CategorySummaries { get; set; } = new();
        public List<Operation> RecentOperations { get; set; } = new();
    }

    public class CategorySummary
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public CategoryType Type { get; set; }
        public decimal TotalAmount { get; set; }
        public int OperationCount { get; set; }
        public decimal Percentage { get; set; }
    }
}
