namespace HW2
{
    public class Report
    {
        private readonly IAccountAnalysisService _analysisService;

        public Report(IAccountAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        public void PrintAccountAnalysis(int accountId)
        {
            var analysis = _analysisService.AnalyzeAccount(accountId);
            PrintAccountAnalysis(analysis);
        }

        public void PrintAccountAnalysis(int accountId, DateTime fromDate, DateTime toDate)
        {
            var analysis = _analysisService.AnalyzeAccount(accountId, fromDate, toDate);
            PrintAccountAnalysis(analysis);
        }

        public void PrintAccountAnalysis(AccountAnalysisResult analysis)
        {
            Console.WriteLine("=== АНАЛИЗ СЧЕТА ===");
            Console.WriteLine($"Счет: {analysis.AccountName} (ID: {analysis.AccountId})");
            Console.WriteLine($"Текущий баланс: {analysis.CurrentBalance:C}");
            Console.WriteLine($"Дата анализа: {analysis.AnalysisDate:dd.MM.yyyy HH:mm}");

            if (analysis.PeriodStart.HasValue && analysis.PeriodEnd.HasValue)
            {
                Console.WriteLine($"Период: {analysis.PeriodStart.Value:dd.MM.yyyy} - {analysis.PeriodEnd.Value:dd.MM.yyyy}");
            }

            Console.WriteLine("\n=== СВОДКА ===");
            Console.WriteLine($"Всего операций: {analysis.TotalOperations}");
            Console.WriteLine($"Общий доход: {analysis.TotalIncome:C}");
            Console.WriteLine($"Общие расходы: {analysis.TotalExpenses:C}");
            Console.WriteLine($"Изменение баланса: {analysis.TotalIncome - analysis.TotalExpenses:C}");

            if (analysis.CategorySummaries.Any())
            {
                Console.WriteLine("\n=== ПО КАТЕГОРИЯМ ===");
                foreach (var category in analysis.CategorySummaries)
                {
                    Console.WriteLine($"{category.CategoryName} ({category.Type}): {category.TotalAmount:C} ({category.OperationCount} операций, {category.Percentage}%)");
                }
            }

            if (analysis.RecentOperations.Any())
            {
                Console.WriteLine("\n=== ПОСЛЕДНИЕ ОПЕРАЦИИ ===");
                foreach (var operation in analysis.RecentOperations)
                {
                    var sign = operation.Type == CategoryType.Income ? "+" : "-";
                    Console.WriteLine($"{operation.Date:dd.MM.yyyy} {sign}{operation.Amount:C} - {operation.Description ?? "Без описания"}");
                }
            }
        }

        public void PrintCategoryReport(int accountId, int categoryId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var operations = _analysisService.GetOperationsByCategory(accountId, categoryId, fromDate, toDate);
            
            Console.WriteLine("=== ОТЧЕТ ПО КАТЕГОРИИ ===");
            Console.WriteLine($"Категория ID: {categoryId}");
            if (fromDate.HasValue && toDate.HasValue)
            {
                Console.WriteLine($"Период: {fromDate.Value:dd.MM.yyyy} - {toDate.Value:dd.MM.yyyy}");
            }
            Console.WriteLine($"Всего операций: {operations.Count}");
            Console.WriteLine($"Общая сумма: {operations.Sum(op => op.Amount):C}");
            Console.WriteLine();

            foreach (var operation in operations)
            {
                var sign = operation.Type == CategoryType.Income ? "+" : "-";
                Console.WriteLine($"{operation.Date:dd.MM.yyyy HH:mm} {sign}{operation.Amount:C} - {operation.Description ?? "Без описания"}");
            }
        }

        public void PrintMonthlyReport(int accountId, int year, int month)
        {
            var fromDate = new DateTime(year, month, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            
            var analysis = _analysisService.AnalyzeAccount(accountId, fromDate, toDate);
            
            Console.WriteLine($"=== МЕСЯЧНЫЙ ОТЧЕТ ===");
            Console.WriteLine($"Счет: {analysis.AccountName}");
            Console.WriteLine($"Период: {fromDate:MMMM yyyy}");
            Console.WriteLine($"Начальный баланс: {analysis.CurrentBalance - analysis.TotalIncome + analysis.TotalExpenses:C}");
            Console.WriteLine($"Конечный баланс: {analysis.CurrentBalance:C}");
            Console.WriteLine($"Изменение: {analysis.TotalIncome - analysis.TotalExpenses:C}");
            Console.WriteLine();

            PrintAccountAnalysis(analysis);
        }
    }
}