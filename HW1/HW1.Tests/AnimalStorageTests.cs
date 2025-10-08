namespace HW1.Tests;

public class AnimalStorageTests
{
    [Fact]
    public void AddAnimalTest()
    {
        var storage = new AnimalStorage(new VetClinic());
        var AnimalHealthy = new TestAnimal(10, 1, "Animal", 10);
        storage.AddAnimal(AnimalHealthy);
        Assert.Collection(storage.Animals, animal => Assert.Equal(AnimalHealthy, animal));

        var AnimalUnhealthy = new TestAnimal(10, 1, "Animal", 0);
        storage.AddAnimal(AnimalUnhealthy);
        Assert.DoesNotContain(AnimalUnhealthy, storage.Animals);
    }

    [Fact]
    public void FoodCountTest()
    {
        var storage = new AnimalStorage(new VetClinic());
        var Animal1 = new TestAnimal(10, 1, "Animal", 10);
        storage.AddAnimal(Animal1);
        Assert.Equal(10, storage.FoodCount());

        var Animal2 = new TestAnimal(20, 2, "Animal", 10);
        storage.AddAnimal(Animal2);
        Assert.Equal(30, storage.FoodCount());
    }

    [Fact]
    public void ContactAnimalsTest()
    {
        var storage = new AnimalStorage(new VetClinic());
        var HerboAnimal = new TestKindAnimal(10, 1, "HerboAnimal", 10, 10);
        storage.AddAnimal(HerboAnimal);
        var PredatorAnimal = new TestPredatorAnimal(10, 1, "PredatorAnimal", 10);
        storage.AddAnimal(PredatorAnimal);
        var result = storage.ContactAnimals(5);
        Assert.Collection(result, animal => Assert.Equal(HerboAnimal, animal));

    }

    [Fact]
    public void AnimalsCountTest()
    {
        var storage = new AnimalStorage(new VetClinic());
        var Animal1 = new TestAnimal(10, 1, "Animal", 10);
        storage.AddAnimal(Animal1);
        Assert.Equal(1, storage.AnimalsCount());

        var Animal2 = new TestAnimal(10, 2, "Animal", 10);
        storage.AddAnimal(Animal2);
        Assert.Equal(2, storage.AnimalsCount());
    }
}