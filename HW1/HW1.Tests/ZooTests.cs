namespace HW1.Tests;

public class ZooTests
{
    [Fact]
    public void Zoo_AddAnimal_And_AddThing_ForwardsToStorages()
    {
        var clinic = new VetClinic();
        var animalStorage = new AnimalStorage(clinic);
        var thingStorage = new ThingStorage();
        var zoo = new Zoo(clinic, animalStorage, thingStorage);

        var a = new TestAnimal(5, 1, "A", 9);
        var t = new TestThing(10, "T");

        zoo.AddAnimal(a);
        zoo.AddThing(t);

        Assert.Contains(a, animalStorage.Animals);
        Assert.Contains(t, thingStorage.Things);

        // Просто исполняем строки, без проверок вывода:
        zoo.AnimalsCount();
        zoo.AnimalsInfo();
        zoo.ContactAnimals();     // default threshold
        zoo.FoodCount();
        zoo.CheckInventory();
    }
}