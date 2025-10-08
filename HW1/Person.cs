class Person : IAlive
{
    public int Food { get; set; }
    public string Name { get; private set; }

    public Person(int food, string name)
    {
        Food = food;
        Name = name;
    }
}