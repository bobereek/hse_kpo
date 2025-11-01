# HW2 — Finance accounting module


Основные функции
- Создание и хранение банковских счетов, категорий, операций.
- Бизнес-логика: переводы между счетами, создание повторяющихся операций, пересчёт баланса.
- Анализ и отчёты по счёту и по категориям.
- Импорт (CSV/JSON/YAML) через Template Method.
- Экспорт (CSV/JSON/YAML) через Adapter pattern.

Структура
- `HW2/Program.cs` — краткая демонстрация (Main).
- `HW2/Accounts` — BankAccount, AccountStorage, интерфейсы.
- `HW2/Operations` — Operation, OperationStorage.
- `HW2/Categories` — Category, CategoryStorage.
- `HW2/Analysis` — AccountAnalysisService и модели отчётов.
- `HW2/Import` — Csv/Json/Yaml importers (Template Method).
- `HW2/Export` — Export adapters + ExportService (Adapter pattern).
- `HW2/FinancialService.cs` — бизнес-логика (переводы, recurring, recalc).

Применённые паттерны


## Коротко: SOLID / GRASP / GoF (mapping)

SOLID
- SRP: хранилища (`Accounts/AccountStorage.cs`, `Operations/OperationStorage.cs`, `Categories/CategoryStorage.cs`), `FinancialService.cs`, `AccountAnalysisService.cs`, `Report.cs`, импортеры/экспортеры.
- OCP: `ExportAdapterFactory.cs`, `FileImportTemplate.cs` (новые форматы без правки ядра).
- LSP: интерфейсы хранилищ (`IBankAccountStorage`, `IOperationStorage`, `ICategoryStorage`).
- ISP: узкие интерфейсы (`IAccountAnalysisService`, `IAccountStorage` и т.д.).
- DIP: конструкторная инъекция — `FinancialService`, `AccountAnalysisService`, `ImportServiceFactory`.

GRASP
- Information Expert: `AccountAnalysisService`, хранилища.
- Creator: `Factory/BankFactory.cs`.
- Controller: `BankFacade.cs`, `Program.cs` (точка входа).
- Low Coupling / High Cohesion: хранилища и сервисы.
- Indirection: `ImportServiceFactory`, `ExportAdapterFactory`.
- Polymorphism: импортёры и адаптеры.

GoF
- Factory: `Factory/BankFactory.cs`, `Export/ExportAdapterFactory.cs` — центральное создание объектов/адаптеров.
- Template Method: `Import/FileImportTemplate.cs` + `Import/CsvImportService.cs`, `Import/JsonImportService.cs`, `Import/YamlImportService.cs`.
- Adapter: `Export/IExportAdapter.cs`, `Export/ExportAdapters.cs`, используется через `ExportService`.
- Facade: `BankFacade.cs` — упрощённый внешний интерфейс.
- Command + Decorator: `Command/ICommand.cs`, `Command/DelegateCommand.cs`, `Command/TimedCommandDecorator.cs` — сценарии и измерение времени.
- DI: `Microsoft.Extensions.DependencyInjection` в `Program.cs`

2. Как запустить

Требования: .NET 9 SDK (проект таргетит net9.0). В Windows PowerShell:

```powershell
dotnet build "HW2\HW2\HW2.csproj"
dotnet run --project "HW2\HW2\HW2.csproj" --configuration Debug
```
