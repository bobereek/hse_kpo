using System;
using Microsoft.Extensions.DependencyInjection;

namespace HW2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<IBankAccountFactory, BankFactory>()
                .AddSingleton<ICategoryFactory, BankFactory>()
                .AddSingleton<IOperationFactory, BankFactory>()
                .AddSingleton<IBankAccountStorage, AccountStorage>()
                .AddSingleton<ICategoryStorage, CategoryStorage>()
                .AddSingleton<IOperationStorage, OperationStorage>()
                .AddSingleton<IAccountAnalysisService, AccountAnalysisService>()
                .AddSingleton<FinancialService>()
                .AddSingleton<Report>()
                .AddSingleton<ImportServiceFactory>()
                .AddSingleton<ExportService>()
                .AddSingleton<BankFacade>()
                .BuildServiceProvider();

            var accountStorage = services.GetRequiredService<IBankAccountStorage>();
            var categoryStorage = services.GetRequiredService<ICategoryStorage>();
            var operationStorage = services.GetRequiredService<IOperationStorage>();
            var financialService = services.GetRequiredService<FinancialService>();
            var report = services.GetRequiredService<Report>();

            accountStorage.AddBankAccount(1, "Main", 0);
            accountStorage.AddBankAccount(2, "Savings", 0);

            categoryStorage.AddCategory(1, "Salary", CategoryType.Income);
            categoryStorage.AddCategory(2, "Groceries", CategoryType.Expense);

            operationStorage.AddOperation(1, CategoryType.Income, 1, 2000m, DateTime.Now.AddDays(-2), 1, "Salary payment");
            operationStorage.AddOperation(2, CategoryType.Expense, 1, 300m, DateTime.Now.AddDays(-1), 2, "Groceries");

            financialService.RecalculateBalance(1);

            report.PrintAccountAnalysis(1);

        }
    }
}