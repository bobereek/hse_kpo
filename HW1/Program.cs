namespace HW1;

using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IClinic, VetClinic>();
        services.AddSingleton<IAnimalStorage>(sp => new AnimalStorage(sp.GetRequiredService<IClinic>()));
        services.AddSingleton<IThingStorage, ThingStorage>();
        services.AddSingleton<Zoo>(sp => new Zoo(
            sp.GetRequiredService<IClinic>(),
            sp.GetRequiredService<IAnimalStorage>(),
            sp.GetRequiredService<IThingStorage>()
        ));

        using var provider = services.BuildServiceProvider();
        var zoo = provider.GetRequiredService<Zoo>();

        // Демо
        Console.WriteLine("Добавление животных");
        zoo.AddAnimal(new Rabbit(3, 101, "Bugs", 8, 8));
        zoo.AddAnimal(new Monkey(4, 102, "Abu", 7, 7));
        zoo.AddAnimal(new Tiger(7, 201, "ShereKhan", 9));
        zoo.AddAnimal(new Wolf(5, 202, "Akela", 6));
        zoo.AddAnimal(new Rabbit(2, 103, "Fluffy", 3, 4)); // Больное животное - будет отклонено
        
        Console.WriteLine("\nДобавление вещей");
        zoo.AddThing(new Table(301, "FeedingTable"));
        zoo.AddThing(new Computer(401, "OfficePC"));
        zoo.AddThing(new Table(302, "VetTable"));

        Console.WriteLine("\nОТЧЕТЫ");
        Console.WriteLine("\n1. Количество животных:");
        zoo.AnimalsCount();

        Console.WriteLine("\n2. Информация о животных:");
        zoo.AnimalsInfo();

        Console.WriteLine("\n3. Контактный зоопарк (доброта > 5):");
        zoo.ContactAnimals();

        Console.WriteLine("\n4. Контактный зоопарк (доброта > 7):");
        zoo.ContactAnimals(7);

        Console.WriteLine("\n5. Суммарное потребление еды:");
        zoo.FoodCount();

        Console.WriteLine("\n6. Инвентаризация:");
        zoo.CheckInventory();
    }
}
