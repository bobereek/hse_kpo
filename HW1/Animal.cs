public abstract class Animal : IAlive, IInventory
{
    public int Food { get; set; }
    public int Number { get; set; }

    public int Health { get; set; }

    public string Name { get;}

    public Animal(int food, int number, string name, int health = 10)
    {
        Food = food;
        Number = number;
        Name = name;
        Health = health;
    }

}