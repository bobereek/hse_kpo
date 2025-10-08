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

    }
}
