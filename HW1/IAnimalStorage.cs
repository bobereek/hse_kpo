public interface IAnimalStorage : IStorage
{
    void AddAnimal(Animal animal);
    int AnimalsCount();
    void AnimalsInfo();
    List<Animal> ContactAnimals(int threshold);
    int FoodCount();
}

