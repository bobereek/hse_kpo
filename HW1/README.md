# HW1 — Zoo ERP (Домашнее задание)

Коротко: консольное приложение, доменная модель зоопарка, принципы SOLID, DI-контейнер, простые отчёты.

## Архитектура и SOLID

- SRP: 
  - `Zoo` — оркестратор, не хранит коллекции, делегирует `IAnimalStorage`, `IThingStorage`.
  - `AnimalStorage` управляет животными, `ThingStorage` — вещами.
- OCP:
  - Добавление новых животных/вещей не требует правок существующих классов.
  - Контактный зоопарк фильтруется по абстракции `IKind`.
- LSP:
  - `Herbo`, `Predator`, `Rabbit`, `Monkey`, `Tiger`, `Wolf` корректно подставимы вместо `Animal`.
- ISP:
  - Гранулярные интерфейсы: `IAlive`, `IInventory`, `IKind`, `IAnimalStorage`, `IThingStorage`.
- DIP:
  - `Zoo` зависит от абстракций `IClinic`, `IAnimalStorage`, `IThingStorage`.
  - `AnimalStorage` зависит от `IClinic`, а не от `Zoo`.

## DI-контейнер

Используется `Microsoft.Extensions.DependencyInjection`.

Регистрации в `Program.cs`:

```csharp
var services = new ServiceCollection();
services.AddSingleton<IClinic, VetClinic>();
services.AddSingleton<IAnimalStorage>(sp => new AnimalStorage(sp.GetRequiredService<IClinic>()));
services.AddSingleton<IThingStorage, ThingStorage>();
services.AddSingleton<Zoo>(sp => new Zoo(
    sp.GetRequiredService<IClinic>(),
    sp.GetRequiredService<IAnimalStorage>(),
    sp.GetRequiredService<IThingStorage>()
));
```

## Запуск

1. `dotnet build`
2. `dotnet run`

В `Program.Main` добавлены демо-данные и вызовы отчётов:

- количество животных
- информация о животных
- контактный зоопарк (по умолчанию порог 5)
- суммарное потребление еды
- инвентаризация (имя + инв. номер животных и вещей)

### Пример вывода

```
Animals count: 4
Rabbit Name=Bugs, FoodPerDayKg=3, InvNo=101, Health=8
Monkey Name=Abu, FoodPerDayKg=4, InvNo=102, Health=7
Tiger Name=ShereKhan, FoodPerDayKg=7, InvNo=201, Health=9
Wolf Name=Akela, FoodPerDayKg=5, InvNo=202, Health=6
Contact zoo animals (Kindness > 5):
Rabbit Name=Bugs, Kindness=8
Monkey Name=Abu, Kindness=7
Total food per day (kg): 19
Inventory: Animal Rabbit Name=Bugs No=101
Inventory: Animal Monkey Name=Abu No=102
Inventory: Animal Tiger Name=ShereKhan No=201
Inventory: Animal Wolf Name=Akela No=202
Inventory: Thing Name=FeedingTable No=301
Inventory: Thing Name=OfficePC No=401
```

### Порог контактного зоопарка

Порог доброты можно менять параметром `threshold`:

```csharp
zoo.ContactAnimals(threshold: 7);
```

Только `Herbo`-животные реализуют `IKind` и попадают в отбор по `Kindness`.

## Тесты

Фактическое покрытие тестами: **84.55%** (208/246 statements covered).

Покрытые сценарии:

- `VetClinic.IsHealthy` — проверка порогов здоровья.
- `AnimalStorage` — приемка только здоровых, подсчет еды (`FoodCount`), отбор в контактный зоопарк по `Kindness` (возвращаемый список), счетчик животных (`AnimalsCount`).
- `ThingStorage` — добавление вещей.
- Легкий смоук `Zoo` — проксирование вызовов в стореджи.

Как посмотреть покрытие локально:

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutput=./coverage/coverage.xml /p:CoverletOutputFormat=cobertura
# По желанию: HTML-отчет
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:./coverage/coverage.xml -targetdir:./coverage-report
# Открыть отчет
# Windows PowerShell:
Start-Process ./coverage-report/index.html
```

