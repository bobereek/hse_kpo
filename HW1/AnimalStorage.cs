public class AnimalStorage: IAnimalStorage
{
    private readonly IClinic clinic;
    private List<Animal> storage = new List<Animal>();

    public List<Animal> Animals => storage;

    public AnimalStorage(IClinic clinic)
    {
        this.clinic = clinic;
    }

    public void AddAnimal(Animal animal)
    {
        if (clinic.IsHealthy(animal))
        {
            storage.Add(animal);
        }
        else
        {
            Console.WriteLine("Reject: " + animal.GetType().Name + " '" + animal.Name + "' is not healthy (Health=" + animal.Health + ")");
        }
    }

    public int AnimalsCount()
    {
        Console.WriteLine("Animals count: " + storage.Count);
        return storage.Count;
    }

    public void AnimalsInfo()
    {
        foreach (var animal in storage)
        {
            Console.WriteLine(
                animal.GetType().Name +
                " Name=" + animal.Name +
                ", FoodPerDayKg=" + animal.Food +
                ", InvNo=" + animal.Number +
                ", Health=" + animal.Health
            );
        }
    }

    public int FoodCount()
    {
        int count = 0;
        foreach (var animal in storage)
        {
            count += animal.Food;
        }
        Console.WriteLine("Total food per day (kg): " + count);

        return count;
    }

    public List<Animal> ContactAnimals(int threshold)
    {
        Console.WriteLine("Contact zoo animals (Kindness > " + threshold + "):");
        List<Animal> contactAnimals = new List<Animal>();
        foreach (var animal in storage)
        {
            if (animal is IKind kindAware && kindAware.Kindness > threshold)
            {
                Console.WriteLine(animal.GetType().Name + " Name=" + animal.Name + ", Kindness=" + kindAware.Kindness);
                contactAnimals.Add(animal);
            }
        }

        return contactAnimals;
    }

    public void CheckInventory()
    {
        foreach (var animal in storage)
        {
            Console.WriteLine("Inventory: Animal " + animal.GetType().Name + " Name=" + animal.Name + " No=" + animal.Number);
        }
    }

    
}