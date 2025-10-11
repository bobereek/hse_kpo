public class Zoo
{
    IAnimalStorage animals_storage;
    IThingStorage things_storage;
    public IClinic Clinic { get; private set; }
    public Zoo(IClinic clinic, IAnimalStorage animalStorage, IThingStorage thingStorage)
    {
        Clinic = clinic;
        animals_storage = animalStorage;
        things_storage = thingStorage;
    }

    public void AddAnimal(Animal animal)
    {
        animals_storage.AddAnimal(animal);
    }

    public void AddThing(Thing thing)
    {
        things_storage.AddThing(thing);
    }

    public void AnimalsCount()
    {
        animals_storage.AnimalsCount();
    }

    public void AnimalsInfo()
    {
        animals_storage.AnimalsInfo();
    }

    public void ContactAnimals(int threshold = 5)
    {
        animals_storage.ContactAnimals(threshold);
    }

    public void FoodCount()
    {
        animals_storage.FoodCount();
    }

    public void CheckInventory()
    {
        animals_storage.CheckStorage();
        things_storage.CheckStorage();
    }

}